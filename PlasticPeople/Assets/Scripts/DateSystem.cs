using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DateSystem : MonoBehaviour {

    private const int TIMESCALE = 45;

    private DayOfWeek dayofweek;
    private double day, second, month, year;

    public Text dayInWeekText, dayText, monthText, yearText; 

	// Use this for initialization
	void Start () {
        month = System.DateTime.Now.Month;
        day = DateTime.Now.Day;
        dayofweek = DateTime.Now.DayOfWeek;
        year = DateTime.Now.Year;
        

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void CalculateMonth()
    {

    }

    void CalculateDay()
    {

    }

}
