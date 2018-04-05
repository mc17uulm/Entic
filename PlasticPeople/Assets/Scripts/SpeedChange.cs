using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SpeedChange : MonoBehaviour {

    public Slider slider;
    public Sprite pauseImage;
    public Sprite playImage;
    public Sprite fastplayImage;
    public float fastTimeScale;
    public TextMeshProUGUI dateText;
    //private float startTime;
    //public DateTime date;
    public Slider yearBar;
    public int factor;

    void Start()
    {
        dateText.text = DateTime.Now.Date.ToString("dd.MM.yyyy");
        factor = 1;
    }

    void Update()
    {
        /**float currentTime = Time.time - startTime;
        DateTime ingameDate = date.AddDays(6 * currentTime);
        dateText.text = ingameDate.ToString("dd.MM.yyyy");
        yearBar.value = ingameDate.DayOfYear;*/
    }

    public int GetFactor()
    {
        return factor;
    }

    public void changeDate(string date)
    {
        dateText.text = date;
    }

    public void OnChange()
    {
        if(slider.value == 0)
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = pauseImage;
            factor = 0;
            //Time.timeScale = 0f;
        }
        else if(slider.value == 1)
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = playImage;
            factor = 1;
            //Time.timeScale = 1f;
        }
        else
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = fastplayImage;
            factor = 3;
            //Time.timeScale = fastTimeScale;
        }
    }

}
