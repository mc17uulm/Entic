using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class progresstest : MonoBehaviour {


    public Image progressImage;
    public Image skill;
    public Image skillIcon;
    public GameObject mask;
    public int progressSpeed;
    private float startTime;

	// Use this for initialization
	void Awake () {
        startTime = Time.time;
        progressImage.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //float currentTime = (float)(Math.Truncate((double)Time.time-startTime * 100)/100);
        float currentTime = Time.time - startTime;
        //Debug.Log("This is the time:" + currentTime);
        if(currentTime <= progressSpeed)
        {
            progressImage.fillAmount = currentTime / progressSpeed;
        }
        else if(currentTime > progressSpeed)
        {
            mask.SetActive(false);
            skill.sprite = Resources.Load<Sprite>("skill");
            skill.SetNativeSize();
            skillIcon.sprite = Resources.Load<Sprite>("skill-icon-done");
        }
	}
}
