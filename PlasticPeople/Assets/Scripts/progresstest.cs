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

    private LinkedList<Logic.Action> actions;
    private bool active;
    private int i;

	// Use this for initialization
	void Awake () {
        Debug.Log("progressTest awake");
        startTime = Time.time;
        progressImage.fillAmount = 0;
        actions = new LinkedList<Logic.Action>();
        active = false;
        i = 0;
	}

    void Start()
    {
        Debug.Log("progressTest start");
    }

    public void Tick()
    {
        if(actions == null)
        {
            Awake();
        }
        LinkedList<Logic.Action> collection = Game.play.GetActions();
        foreach(Logic.Action a in collection)
        {
            if (a.GetState().Equals(ActionClick.State.InDevelopement))
            {
                if (!this.actions.Contains(a))
                {
                    this.actions.AddLast(a);
                }
            }
        }

        float x = 0, y = 0;
        foreach(Logic.Action a in actions)
        {
            switch (a.GetState())
            {
                case ActionClick.State.InDevelopement:
                    x += a.GetStart();
                    y += a.GetDevelopment();
                    if(actions.Count == 1)
                    {
                        active = true;
                        mask.SetActive(true);
                        skill.sprite = Resources.Load<Sprite>("skill-background");
                        skill.SetNativeSize();
                        skillIcon.sprite = Resources.Load<Sprite>("skill-icon");
                    }
                    break;

                case ActionClick.State.Executed:
                    actions.Remove(a);
                    i -= a.GetDevelopment() * 30;
                    Debug.Log("I after Development: " + i);
                    if(actions.Count == 0)
                    {
                        i = 0;
                        active = false;
                        mask.SetActive(false);
                        skill.sprite = Resources.Load<Sprite>("skill");
                        skill.SetNativeSize();
                        skillIcon.sprite = Resources.Load<Sprite>("skill-icon-done");
                    }
                    break;
            }
        }

        if (active)
        {

            float progress = i / (y*30);
            Debug.Log("Count: " + actions.Count);
            Debug.Log("Progress: " + progress);
            Debug.Log("i: " + i);
            Debug.Log("y: " + y);

            progressImage.fillAmount = progress;
            i += actions.Count;
        }
        
    }
	
	// Update is called once per frame
    void Update()
    {
        //float currentTime = (float)(Math.Truncate((double)Time.time-startTime * 100)/100);

    }
}
