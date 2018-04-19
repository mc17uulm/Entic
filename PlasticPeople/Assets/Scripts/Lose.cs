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
	    int f = 0;
	    foreach (Action a in Game.play.GetEndActions())
	    {
	        if (a.GetState().Equals(ActionClick.State.Developed))
	        {
	            f++;
	        }
	    }

	    money.text = mny.Replace("[x]", f.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
