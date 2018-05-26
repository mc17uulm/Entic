using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Logic;

public class Lose : MonoBehaviour
{

    public TextMeshProUGUI subtitle;
    public TextMeshProUGUI money;

	// Use this for initialization
	void Awake ()
	{
	    string sbt = subtitle.text;
	    string mny = money.text;

	    int year = Game.play.GetEndTime().Year;
	    year = year - 2019;
	    subtitle.text = sbt.Replace("[x]", year.ToString());

	    money.text = mny.Replace("[x]", Game.play.GetEndActions().ToString("N"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
