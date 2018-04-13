﻿using System;
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
    public int i, y;
    private bool c;
    private Stopwatch watch;
    public static System.Random random;


	// Use this for initialization
	void Awake () {
        watch = Stopwatch.StartNew();
        //UnityEngine.Debug.Log("Loaded Game.cs");
        string path = Application.streamingAssetsPath + "/countries.json";
        string countries = File.ReadAllText(path);
        Country[] countryarr = JsonHelper.FromJson<Country>(countries);
        LinkedList<Logic.Country> countrylist = new LinkedList<Logic.Country>();
        float amount = 0, production = 0;
        InfoPanel panel = FindObjectOfType<InfoPanel>();
        Newsticker news = FindObjectOfType<Newsticker>();
        infoBox = FindObjectOfType<ActionInfoBox>();
        random = new System.Random();

        foreach (Country country in countryarr)
        {
            try
            {
                countrylist.AddLast(new Logic.Country(
                    Int32.Parse(country.id),
                    country.code,
                    country.name,
                    Int32.Parse(country.population.Replace(",", "")),
                    country.description,
                    float.Parse(country.waste, CultureInfo.InvariantCulture.NumberFormat),
                    country.rate,
                    random
                ));
                amount += float.Parse(country.waste, CultureInfo.InvariantCulture.NumberFormat);
                production += country.rate;
            }
            catch (FormatException e)
            {
                UnityEngine.Debug.Log(e.Message);
            }
        }
        play = new Play(countrylist, news);
        play.SetActions(ReadInActions());
        last = DateTime.Now;
        print = DateTime.Parse("2019-01-01 00:00:00");
        change = FindObjectOfType<SpeedChange>();
        i = 1;
        y = 1;
        panel.changeWaste(amount, production);
        panel.changeCapital(30000000, 2500000);
        //change.yearBar.maxValue = DateTime.DaysInMonth(print.Year, print.Month);
        news.AddNews(new News("Start", "Producing this much of plastic waste will exterminate humans til 2070.", NewsType.Emergency));
        c = false;
        double now = watch.ElapsedMilliseconds;
        //UnityEngine.Debug.Log("Load Awake: " + (now / 1000) + " seconds");
        play.Tick();
    }

    // Update is called once per frame
    void Update () {
        if((change.factor != 0) && (!c) && (last.AddSeconds(1.0/7/ change.GetFactor()) <= DateTime.Now))
        {
            c = true;
            int daysInMonth = DateTime.DaysInMonth(print.Year, print.Month);
            change.changeDate(print.ToString("dd.MM.yyyy"));
            //change.yearBar.maxValue = daysInMonth;
            change.yearBar.value = y;
            //UnityEngine.Debug.Log(y);
            if ((i == daysInMonth) && (y == 365))
            {
                y = 1;
                i = 1;
                play.Tick();
            }
            else if ((i == daysInMonth) && (y != 365))
            {
                i = 1;
                play.Tick();
            }
            else if ((i != daysInMonth) && (y >= 365))
            {
                y = 1;
            }
            else
            {
                y++;
                i++;
            }
            print = print.AddDays(1);
            last = DateTime.Now;
            c = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            play.Win();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            play.Lose();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Environment.Exit(0);
        }
	}

    public LinkedList<Logic.Action> ReadInActions()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/actions.json");
        LinkedList<Logic.Action> o = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedList<Logic.Action>>(json);

        foreach(Logic.Action a in o)
        {
            UnityEngine.Debug.Log(a.GetName() + ": " + a.IsActivated());
        }

        return o;
    }

    public DateTime GetPrint()
    {
        return print;
    }
    
}
