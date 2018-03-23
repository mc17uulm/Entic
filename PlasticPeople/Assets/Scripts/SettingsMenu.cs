using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject blurEffect;
    public GameObject darkenEffect;

	public void SetMusicVolume (float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSoundVolume (float soundVolume)
    {
        audioMixer.SetFloat("SoundVolume", soundVolume);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        blurEffect.SetActive(false);
        darkenEffect.SetActive(false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        blurEffect.SetActive(true);
        darkenEffect.SetActive(true);
    }
}
