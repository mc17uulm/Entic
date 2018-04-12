using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Logic;

public class Skilltree : MonoBehaviour
{


    public static bool GameIsPaused = false;

    public GameObject skilltreeUI;
    public GameObject blurEffect;
    public GameObject darkenEffect;
    public Slider speedControl;
    public TextMeshProUGUI lobbyText;
    public GameObject action;

    public void Resume()
    {
        skilltreeUI.SetActive(false);
        SpeedChange change = FindObjectOfType<SpeedChange>();
        change.Resume();
        GameIsPaused = false;
        blurEffect.SetActive(false);
        darkenEffect.SetActive(false);
    }

    public void Pause()
    {
        skilltreeUI.SetActive(true);
        Image[] l = action.GetComponentsInChildren<Image>();
        //Debug.Log("Length: " + l.Length);
        LinkedList<Action> actions = Game.play.GetActions();
        HashSet<int> unique = new HashSet<int>();
        foreach (Action a in actions)
        {
            if (a.IsFinished())
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
                        if ((!a.IsExecuted()) && (!a.IsInDevelopment())){
                            im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-1");
                            a.ChangeState(ActionClick.State.Clickable);
                        }
                        else if (a.IsInDevelopment())
                        {
                            im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-3");
                        }
                        else if (a.IsExecuted())
                        {
                            im.sprite = Resources.Load<Sprite>("Actions/" + a.GetId() + "-2");
                        }
                    }
                }
            }
        }
        SpeedChange change = FindObjectOfType<SpeedChange>();
        change.Pause();
        lobbyText.text = Game.play.GetLobby().PrintStatus();
        GameIsPaused = true;
        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);
        Game.play.SetActions(actions);
    }
}

