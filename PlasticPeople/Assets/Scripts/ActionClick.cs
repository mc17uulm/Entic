using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Logic;

public class ActionClick : MonoBehaviour, IPointerClickHandler {

    private LinkedList<Action> actions;
    private GameObject lastClicked;
    public Sprite clicked;
    public Sprite normal;

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Clicked on Action");

        // Der Name des Objektes
        string element = data.pointerEnter.name;

        /**Action selected;
        foreach(Action action in actions)
        {
            if (action.GetName().Equals(element))
            {
                selected = action;
            }
            else
            {
                throw new System.Exception("Clickable Element without matching data");
            }
        }*/

        if(lastClicked == null)
        {
            lastClicked = data.pointerEnter;
            lastClicked.GetComponent<Image>().color = new Color32(234, 206, 87, 255);
        }
        else
        {
            lastClicked.GetComponent<Image>().color = new Color32(194, 194, 194, 255);
            lastClicked = data.pointerEnter;
            lastClicked.GetComponent<Image>().color = new Color32(234, 206, 87, 255);
        }

        
    }

	// Use this for initialization
	void Awake () {
        actions = new LinkedList<Action>();
        lastClicked = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
