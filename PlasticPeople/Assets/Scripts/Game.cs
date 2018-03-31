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
        Logic.Country[] countryarr = JsonHelper.FromJson<Logic.Country>(countries);
        LinkedList<Logic.Country> countrylist = new LinkedList<Logic.Country>();
        Debug.Log("Arr Size:" + countryarr.Length);
        foreach(Logic.Country country in countryarr)
        {
            Debug.Log(country.GetName());
            countrylist.AddLast(country);
        }
        //Debug.Log("List:");
        //Debug.Log(countrylist);
        play = new Play(countrylist);
        last = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
        if(last.AddSeconds(1.0) <= DateTime.Now)
        {
            //play.Tick();
        }
	}
}
