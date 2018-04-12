using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour {

    public TextMeshProUGUI capitalText;
    public TextMeshProUGUI capitalChangeText;
    public TextMeshProUGUI wasteText;
    public TextMeshProUGUI wasteChangeText;
    public Image capitalIndicator;
    public Image wasteIndicator;

    public void changeCapital(double sum, double change)
    {
        string exponential = string.Format("{0:E2}", sum);
        string[] parts = exponential.Split('E');
        string amount = parts[0] + " · 10<sup>" + Int32.Parse(parts[1].Substring(1)) + "</sup>";

        double t = Math.Round(change / sum, 2);
        string prefix = "", number = "", c = "";
        if (change < 0)
        {
            prefix = "<color=#b51818>-</color>";
            number = change.ToString("#,0,, million").Remove(0, 1);
            c = t.ToString().Remove(0, 1) + "%";
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#b51818", out color);
            capitalIndicator.color = color;
            capitalIndicator.transform.Rotate(180, 0, 0);
        }
        else
        {
            prefix = "<color=#18b518>+</color>";
            number = change.ToString("#,0,, million");
            c = t.ToString() + "%";
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#18b518", out color);
            capitalIndicator.color = color;
        }
        string changeText = "monthly:  " + prefix + c + "  " + prefix + number;

        capitalText.text = amount;
        capitalChangeText.text = changeText;

    }

    public void changeWaste(double sum, double change)
    {
        string exponential = string.Format("{0:E2}", sum);
        string[] parts = exponential.Split('E');
        string amount = Math.Round((Double.Parse(parts[0])*100), 0) + " · 10<sup>" + (Int32.Parse(parts[1].Substring(1))-2) + "</sup>";

        double t = Math.Round(change / sum, 3);
        string prefix = "", number = "", c = "";
        if(change < 0.0 && change > -1000000000.0)
        {
            prefix = "<color=#18b518>-</color>";
            number = change.ToString("#,0,, million").Remove(0,1);
            c = t.ToString().Remove(0, 1) + "%";
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#18b518", out color);
            wasteIndicator.color = color;
            wasteIndicator.transform.Rotate(180, 0, 0);
        }
        else if(change < -1000000000.0)
        {
            prefix = "<color=#18b518>-</color>";
            number = change.ToString("#,0,, billion").Remove(0, 1);
            c = t.ToString().Remove(0, 1) + "%";
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#18b518", out color);
            wasteIndicator.color = color;
            wasteIndicator.transform.Rotate(180, 0, 0);
        }
        else if(change > 1000000000.0)
        {
            prefix = "<color=#b51818>+</color>";
            number = change.ToString("#,0,, billion");
            c = t.ToString() + "%";
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#b51818", out color);
            wasteIndicator.color = color;
        }
        else
        {
            prefix = "<color=#b51818>+</color>";
            number = change.ToString("#,0,, million");
            c = t.ToString() + "%";
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#b51818", out color);
            wasteIndicator.color = color;
        }
        string changeText = "monthly:  " + prefix + c + "  " + prefix + number;

        wasteText.text = amount;
        wasteChangeText.text = changeText;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
