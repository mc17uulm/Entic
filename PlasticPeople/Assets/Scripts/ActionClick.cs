using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Logic;

public class ActionClick : MonoBehaviour, IPointerClickHandler {

    private LinkedList<Action> actions;
    private GameObject lastClicked;
    public GameObject blurEffect;
    public GameObject darkenEffect;
    public ActionInfoBox infoBox;
    public Sprite clicked;
    public Sprite normal;

    public void OnPointerClick(PointerEventData data)
    {

        // Der Name des Objektes
        string element = data.pointerEnter.name;
        Debug.Log("Clicked on Action: " + element);

        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);

        actions = Game.play.GetActions();
        Skilltree tree = FindObjectOfType<Skilltree>();
        ActionInfoBox infoBox = tree.GetInfoBox();
        
        foreach(Action action in actions)
        {
            if (action.GetId().ToString() == element)
            {
                Debug.Log("Ja: " + action.GetName());
                infoBox.SetActive(true);
                infoBox.ChangeHeader(action.GetName());
            }
            else
            {
                throw new System.Exception("Clickable Element without matching data");
            }
        }

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
        lastClicked = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
