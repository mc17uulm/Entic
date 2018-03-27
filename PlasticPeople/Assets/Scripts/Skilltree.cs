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
        if(speedControl.value == 0f)
        {
            Time.timeScale = 0f;
        }
        else if (speedControl.value == 2f)
        {
            Time.timeScale = 5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        GameIsPaused = false;
        blurEffect.SetActive(false);
        darkenEffect.SetActive(false);
    }

    public void Pause()
    {
        skilltreeUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);
    }
}

