using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpeedChange : MonoBehaviour {

    public Slider slider;
    public Sprite pauseImage;
    public Sprite playImage;
    public Sprite fastplayImage;
    public float fastTimeScale;
    public Text dateText;
    private float startTime;
    private DateTime date;
    public Slider yearBar;

    void Start()
    {
        startTime = Time.time;
        date = DateTime.Now.Date;
        dateText.text = date.ToString("dd.MM.yyyy");
    }

    void Update()
    {
        float currentTime = Time.time - startTime;
        DateTime ingameDate = date.AddDays(6 * currentTime);
        dateText.text = ingameDate.ToString("dd.MM.yyyy");
        yearBar.value = ingameDate.DayOfYear;
    }

    public void OnChange()
    {
        if(slider.value == 0)
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = pauseImage;
            Time.timeScale = 0f;
        }
        else if(slider.value == 1)
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = playImage;
            Time.timeScale = 1f;
        }
        else
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = fastplayImage;
            Time.timeScale = fastTimeScale;
        }
    }

}
