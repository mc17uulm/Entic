using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Globalization;
using UnityEngine;
using Logic;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

public class Game : MonoBehaviour {

    public static Play play;
    public static DateTime last;
    public static DateTime print;
    public static SpeedChange change;
    public static Newsticker news;
    public static ActionInfoBox infoBox;
    public static System.Random random;
    public float elapsed;


	// Use this for initialization
	void Awake () {
        // elapsed is a help var for timing each second
        elapsed = 0f;

        // countries.json contains all country information
        string c = Application.streamingAssetsPath + "/countries.json";

        string countries = File.ReadAllText(c);

        Country[] countryarr = JsonHelper.FromJson<Country>(countries);

        // the json is parsed into a linked list
        LinkedList<Logic.Country> countrylist = new LinkedList<Logic.Country>();
        LinkedList<RandomEvent> eventslist = ReadInEvents();

        float amount = 0, production = 0;

        // Load game objects
        InfoPanel panel = FindObjectOfType<InfoPanel>();
        Newsticker news = FindObjectOfType<Newsticker>();
        infoBox = FindObjectOfType<ActionInfoBox>();
        change = FindObjectOfType<SpeedChange>();

        // with random we give each country a own influence factor (between 0 and 1)
        random = new System.Random();

        // Get each country from the array, create a new Country object and save it in the linked list
        foreach (Country country in countryarr)
        {
            try
            {
                countrylist.AddLast(new Logic.Country(Int32.Parse(country.id), country.code, country.name, Int32.Parse(country.population.Replace(",", "")), country.description, float.Parse(country.waste, CultureInfo.InvariantCulture.NumberFormat), country.rate, random
                ));

                // get the starting values for amount and production of plastic
                amount += float.Parse(country.waste, CultureInfo.InvariantCulture.NumberFormat);
                production += country.rate;
            }
            catch (FormatException err)
            {
                UnityEngine.Debug.Log(err.Message.ToString());
            }
        }

        // create new play object in which most of the game logic  is
        play = new Play(countrylist, eventslist, news);
        play.SetActions(ReadInActions());

        // DateTime objects to map real time to ingame time
        last = DateTime.Now;
        print = DateTime.Parse("2019-01-01 00:00:00");
        
        // add starting values to map
        panel.changeWaste(amount, production);
        panel.changeCapital(30000000, 2500000);
        news.AddNews(new News("Start", "Producing this much of plastic waste will exterminate humans til 2070.", NewsType.Emergency));
        
        // initialize game with first play tick
        play.Tick(print);
    }

    // Update is called once per frame
    void Update () {
        if(change.factor != 0)
        {
            elapsed += Time.deltaTime;
            float factor = 1f / 7 / change.factor;
            if(elapsed >= factor)
            {
                elapsed = elapsed % factor;
                Tick();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            play.Win(false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            play.Lose(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Environment.Exit(0);
        }
	}

    public void Tick()
    {
        print = print.AddDays(1);
        change.changeDate(print.ToString("dd.MM.yyyy"));
        change.yearBar.value = print.DayOfYear;
        play.Tick(print);
    }

    public LinkedList<Logic.Action> ReadInActions()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/actions.json");
        LinkedList<Logic.Action> o = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedList<Logic.Action>>(json);

        /**foreach(Logic.Action a in o)
        {
            UnityEngine.Debug.Log(a.GetName() + ": " + a.GetState());
        }*/

        return o;
    }

    public LinkedList<RandomEvent> ReadInEvents()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/events.json");
        LinkedList<RandomEvent> o = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedList<RandomEvent>>(json);

        return o;
    }

    public DateTime GetPrint()
    {
        return print;
    }
    
}
