using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;

    [HideInInspector]
    public int currentlyPlaying = 0;
	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.group;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    private void Start()
    {
        Play("MainTheme");
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            int tracknumber = UnityEngine.Random.Range(0,3);
            Debug.Log("Random Number is: "+tracknumber);
            currentlyPlaying = tracknumber;
            IEnumerator fadeMusic = AudioFadeOut.FadeOut(Array.Find(sounds, sound => sound.name == "MainTheme").source, 0.8f, Array.Find(sounds, sound => sound.name == "GameLoop"+currentlyPlaying+"").source);
            StartCoroutine(fadeMusic);
        }
        if(level == 0)
        {
            IEnumerator fadeMusic = AudioFadeOut.FadeOut(Array.Find(sounds, sound => sound.name == "GameLoop"+currentlyPlaying+"").source, 0.8f, Array.Find(sounds, sound => sound.name == "MainTheme").source);
            StartCoroutine(fadeMusic);
        }

    }

    public void Play(string name)
    {
        Debug.Log("trying to play "+name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log("found "+s.name+" to play");
        if(s.source == null)
        {
            Debug.Log("The clip with the name "+name+" was not found");
            return;
        }
        Debug.Log("die soße ist folgende: "+s.source.name);
        s.source.Play();
    }

}
