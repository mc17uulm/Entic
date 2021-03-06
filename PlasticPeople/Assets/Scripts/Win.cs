﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Logic;

public class Win : MonoBehaviour {

    public TextMeshProUGUI subtitle;
    public TextMeshProUGUI money;
    public TextMeshProUGUI action;

    // Use this for initialization
    void Start () {
	    string sbt = subtitle.text;
	    string mny = money.text;
        string act = action.text;

	    int year = Game.play.GetEndTime().Year;
	    year = year - 2019;
	    subtitle.text = sbt.Replace("[x]", year.ToString());

        action.text = act.Replace("[x]", Game.play.GetEndActions().ToString());
        money.text = mny.Replace("[x]", Game.play.GetEndCapital().GetAmount().ToString("N"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
