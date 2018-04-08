using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skilltree : MonoBehaviour
{


    public static bool GameIsPaused = false;

    public GameObject skilltreeUI;
    public GameObject blurEffect;
    public GameObject darkenEffect;
    public Slider speedControl;

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
        SpeedChange change = FindObjectOfType<SpeedChange>();
        change.Pause();
        GameIsPaused = true;
        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);
    }
}

