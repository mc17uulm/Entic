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

    public void OnPointerClick(PointerEventData data)
    {

        // Der Name des Objektes
        string element = data.pointerEnter.name;
        Debug.Log("Clicked on Action: " + element);

        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);

        actions = Game.play.GetActions();
        
        foreach(Action action in actions)
        {
            if (action.GetId().ToString() == element)
            {
                active = action;
                Debug.Log("Ja: " + action.GetName());

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
    }

    public void ShowDeactivated(Action action)
    {
        buy.interactable = false;
        headerText.text = action.GetName();
        descrText.text = action.GetDescr();
        costs.text = action.PrintCosts();
        string eff = action.PrintEffects();
        Debug.Log("Back: " + eff);
        effects.text = eff;
        Debug.Log("After: " + effects.text);
        status.text = "<b>Status: " + action.GetState().ToString() + "</b>";
        requirements.text = action.PrintRequirements(actions);
    }

    public void ShowNormal(Action action)
    {

    }

    public void ShowInDevelopment(Action action)
    {

    }

    public void ShowDeveloped(Action action)
    {

    }

	// Use this for initialization
	void Awake () {
        lastClicked = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
