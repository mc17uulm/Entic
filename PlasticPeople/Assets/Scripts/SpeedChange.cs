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
    public int before;

    void Start()
    {
        dateText.text = DateTime.Now.Date.ToString("dd.MM.yyyy");
        factor = 1;
        before = 1;
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
        Debug.Log("Slider Value: " + slider.value);
        if(slider.value == 0)
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = pauseImage;
            before = factor;
            factor = 0;
            //Time.timeScale = 0f;
        }
        else if(slider.value == 1)
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = playImage;
            before = factor;
            factor = 1;
            //Time.timeScale = 1f;
        }
        else
        {
            slider.handleRect.gameObject.GetComponent<Image>().sprite = fastplayImage;
            before = factor;
            factor = 3;
            //Time.timeScale = fastTimeScale;
        }
    }

    public void Pause()
    {
        before = factor;
        factor = 0;
    }

    public void Resume()
    {
        factor = before;
        before = 0;
    }

}
