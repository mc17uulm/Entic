using System;
using System.Collections.Generic;
using UnityEngine;
using Logic;
using System.IO;

public class Game : MonoBehaviour {

    public static Play play;
    public static DateTime last; 


	// Use this for initialization
	void Awake () {
        Debug.Log("Loaded Game.cs");
        string path = Application.streamingAssetsPath + "/countries.json";
        string countries = File.ReadAllText(path);
        Country[] countryarr = JsonHelper.FromJson<Country>(countries);
        LinkedList<Logic.Country> countrylist = new LinkedList<Logic.Country>();
        Debug.Log("Arr Size:" + countryarr.Length);
        System.Random random = new System.Random();
        foreach(Country country in countryarr)
        {
            try
            {
                countrylist.AddLast(new Logic.Country(
                    Int32.Parse(country.id),
                    country.code,
                    country.name,
                    Int32.Parse(country.population.Replace(",", "")),
                    country.description,
                    Int32.Parse(country.waste),
                    country.rate
                ));
            }
            catch (FormatException e)
            {
                Debug.Log(e.Message);
            }
        }
        Debug.Log("List:");
        Debug.Log(countrylist.Count);
        play = new Play(countrylist);
        last = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
        if(last.AddSeconds(1.0) <= DateTime.Now)
        {
            play.Tick();
        }
	}
}
