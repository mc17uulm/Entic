using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

	public void SetMusicVolume (float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSoundVolume (float soundVolume)
    {
        audioMixer.SetFloat("SoundVolume", soundVolume);
    }
}
