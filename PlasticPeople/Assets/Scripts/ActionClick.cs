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
        Activated,
        InDevelopement,
        Developed,
        Executed
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
    public Skilltree tree;
    public Action active;

    public TextMeshProUGUI lobbyText;
    private GameObject selected;
    public GameObject action;

    public void OnPointerClick(PointerEventData data)
    {

        // Der Name des Objektes
        selected = data.pointerEnter.gameObject;
        string element = data.pointerEnter.name;
        //Debug.Log("Clicked on Action: " + element);

        tree = FindObjectOfType<Skilltree>();

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

                    case State.Activated:
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
        Debug.Log("Buy: " + active.GetName());
        if (Game.play.Buy(active))
        {
            Image[] l = action.GetComponentsInChildren<Image>();
            lobbyText.text = Game.play.GetLobby().PrintStatus();
            //selected.GetComponent<Image>().sprite = Resources.Load<Sprite>("Actions/" + active.GetId() + "-3");
            LinkedList<Action> actions = Game.play.GetActions();
            HashSet<int> unique = new HashSet<int>();
            foreach (Action a in actions)
            {
                if (a.GetState().Equals(ActionClick.State.Developed))
                {
                    unique.Add(a.GetId());
                }
            }
            int[] developed = new int[unique.Count];
            unique.CopyTo(developed);
            foreach (Image im in l)
            {
                string[] parts = im.sprite.name.Split('-');
                foreach (Action a in actions)
                {
                    if (parts[0] == a.GetId().ToString())
                    {
                        if (a.Unlocked(developed))
                        {
                            //if((a.GetPrice() <= Game.play.GetCapital().GetAmount()) && (a.GetPoints() <= Game.play.GetLobby().GetAmount())) { 
                            if (a.GetState().Equals(ActionClick.State.Activated))
                            {
                                im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-1");
                                //a.ChangeState(ActionClick.State.Activated);
                            }
                            else if (a.GetState().Equals(ActionClick.State.InDevelopement))
                            {
                                im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-3");
                            }
                            else if ((a.GetState().Equals(ActionClick.State.Developed)) || (a.GetState().Equals(ActionClick.State.Executed)))
                            {
                                im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-2");
                            }

                            if ((a.GetState().Equals(ActionClick.State.Activated)) && ((a.GetPrice() > Game.play.GetCapital().GetAmount()) ||
                                (a.GetPoints() > Game.play.GetLobby().GetAmount())))
                            {
                                im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-0");
                            }
                        }
                    }
                }
            }
            infoBox.SetActive(false);
        }
        else
        {
            Debug.Log("BuyError");
        }
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
