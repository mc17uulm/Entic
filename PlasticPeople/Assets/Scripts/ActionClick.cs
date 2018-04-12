using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Logic;

public class ActionClick : MonoBehaviour, IPointerClickHandler {

    public enum State
    {
        Deactivated,
        Clickable,
        InDevelopement,
        Developed
    }

    private LinkedList<Action> actions;
    private GameObject lastClicked;
    public GameObject blurEffect;
    public GameObject darkenEffect;
    public GameObject infoBox;
    public Button buy;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI descrText;
    public TextMeshProUGUI costs;
    public TextMeshProUGUI effects;
    public TextMeshProUGUI status;
    public TextMeshProUGUI requirements;
    public Action active;

    public TextMeshProUGUI lobbyText;
    public GameObject selected;

    public void OnPointerClick(PointerEventData data)
    {

        // Der Name des Objektes
        selected = data.pointerEnter.gameObject;
        string element = data.pointerEnter.name;
        //Debug.Log("Clicked on Action: " + element);

        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);

        actions = Game.play.GetActions();
        
        foreach(Action action in actions)
        {
            if (action.GetId().ToString() == element)
            {
                active = action;
                //Debug.Log("Name: " + action.GetName() + " State: " + action.GetState());

                switch (action.GetState())
                {

                    case State.Deactivated:
                        ShowDeactivated(action);
                        break;

                    case State.Clickable:
                        ShowNormal(action);
                        break;

                    case State.InDevelopement:
                        ShowInDevelopment(action);
                        break;

                    case State.Developed:
                        ShowDeveloped(action);
                        break;
                }
                headerText.text = action.GetName();
                descrText.text = action.GetDescr();
                costs.text = action.PrintCosts();
                string eff = action.PrintEffects();
                //Debug.Log("Back: " + eff);
                effects.text = eff;
                //Debug.Log("After: " + effects.text);
                status.text = "<b>Status: " + action.GetState().ToString() + "</b>";
                requirements.text = action.PrintRequirements(actions);
                infoBox.SetActive(true);
            }
        }

        
    }

    public void Close()
    {
        infoBox.SetActive(false);
    }

    public void Buy()
    {
        Game.play.Buy(active);
        lobbyText.text = Game.play.GetLobby().PrintStatus();
        selected.GetComponent<Image>().sprite = Resources.Load<Sprite>("Actions/" + active.GetId() + "-3"); 
        infoBox.SetActive(false);
    }

    public void ShowDeactivated(Action action)
    {
        buy.interactable = false;
    }

    public void ShowNormal(Action action)
    {
        if (Game.play.GetCapital().GetAmount() >= action.GetPrice())
        {
            if (Game.play.GetLobby().GetAmount() >= action.GetPoints())
            {
                buy.interactable = true;
            }
        }
    }

    public void ShowInDevelopment(Action action)
    {
        buy.interactable = false;
    }

    public void ShowDeveloped(Action action)
    {
        buy.interactable = false;
        Sprite developed = Resources.Load<Sprite>("/Actions" + action.GetId() + "-2");
        buy.GetComponent<Image>().sprite = developed;
    }
    
	// Use this for initialization
	void Awake () {
        lastClicked = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
