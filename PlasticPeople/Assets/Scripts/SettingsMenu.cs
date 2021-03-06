﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject blurEffect;
    public GameObject darkenEffect;

    private bool soundToggle = true;

	public void SetMusicVolume (float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSoundVolume (float soundVolume)
    {
        audioMixer.SetFloat("SoundVolume", soundVolume);
    }

    public void ToggleVolume(bool isMusic)
    {

        if (isMusic && soundToggle)
        {
            audioMixer.SetFloat("MusicVolume", -80);
            soundToggle = false;
        }
        else if (isMusic && !soundToggle)
        {
            audioMixer.SetFloat("MusicVolume", 0);
            soundToggle = true;
        }
        else if (!isMusic && soundToggle)
        {
            audioMixer.SetFloat("SoundVolume", -80);
            soundToggle = false;
        }
        else
        {
            audioMixer.SetFloat("SoundVolume", 0);
            soundToggle = true;
        }
    }

    public void Resume()
    {
        SpeedChange change = FindObjectOfType<SpeedChange>();
        pauseMenuUI.SetActive(false);
        change.Resume();
        GameIsPaused = false;
        blurEffect.SetActive(false);
        darkenEffect.SetActive(false);
    }

    public void Pause()
    {
        SpeedChange change = FindObjectOfType<SpeedChange>();
        //FindObjectOfType<CountryDisplay>().HideCountryInfo();
        pauseMenuUI.SetActive(true);
        change.Pause();
        GameIsPaused = true;
        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);
    }
}
