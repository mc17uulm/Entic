using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Logic;

public class progresstest : MonoBehaviour {


    public Image progressImage;
    public Image skill;
    public Image skillIcon;
    public GameObject mask;
    public int progressSpeed;
    private float startTime;

    private int bef;

	// Use this for initialization
	void Awake () {
        startTime = Time.time;
        progressImage.fillAmount = 0;
	    bef = 0;
	}

    public void Tick()
    {
        LinkedList<Logic.Action> actions = Game.play.GetActions();
        float sumf = 0.0f, sumn = 0.0f;

        bool active = false;
        foreach (Logic.Action a in actions)
        {
            if (a.GetState().Equals(ActionClick.State.InDevelopement))
            {
                Debug.Log(a.GetName()+ ": " + a.GetState() + " : " + a.GetStart());
                active = true;
                sumf += a.GetDevelopment();
                sumn += a.GetStart();
            }
        }
        if (active)
        {
            float amount = sumn / sumf;
            if (amount == 1.0)
            {
                mask.SetActive(false);
                skill.sprite = Resources.Load<Sprite>("skill");
                skill.SetNativeSize();
                skillIcon.sprite = Resources.Load<Sprite>("skill-icon-done");
            }
            else
            {
                progressImage.fillAmount = amount;
            }
        }
        else
        {
            mask.SetActive(true);
            skill.sprite = Resources.Load<Sprite>("skill-background");
            skill.SetNativeSize();
            skillIcon.sprite = Resources.Load<Sprite>("skill-icon");
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        //float currentTime = (float)(Math.Truncate((double)Time.time-startTime * 100)/100);

    }
}
