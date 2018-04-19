using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

    
    public static GameManager instance = null;
    private int level = 0;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    public void PlayGame()
    {
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void EndGame()
    {
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSoundVolume(float soundVolume)
    {
        audioMixer.SetFloat("SoundVolume", soundVolume);
    }

}
