using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSound : MonoBehaviour {

    public bool isMusic;

    public AudioMixer mixer;

	// Use this for initialization
	void Start () {
        float currVolume = 0;
        if (isMusic)
        {
            mixer.GetFloat("MusicVolume", out currVolume);
            gameObject.GetComponent<Slider>().value = currVolume;
        }
        else
        {
            mixer.GetFloat("SoundVolume", out currVolume);
            gameObject.GetComponent<Slider>().value = currVolume;
        }
	}
	
}
