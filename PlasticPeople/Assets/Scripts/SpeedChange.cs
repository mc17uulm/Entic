using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChange : MonoBehaviour {

    public Slider slider;
    public Sprite pauseImage;
    public Sprite playImage;
    public Sprite fastplayImage;

	public void onChange()
    {
        Debug.Log("triggered");
        if(slider.value == 0)
        {
            Debug.Log("pause");
            slider.handleRect.gameObject.GetComponent<Image>().sprite = pauseImage;
        }
        else if(slider.value == 1)
        {
            Debug.Log("play");
            slider.handleRect.gameObject.GetComponent<Image>().sprite = playImage;
        }
        else
        {
            Debug.Log("gotta go fast");
            slider.handleRect.gameObject.GetComponent<Image>().sprite = fastplayImage;
        }

    }

}
