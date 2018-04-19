using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionInfoBox : MonoBehaviour {

    public GameObject infoBox;

    public void SetActive(bool value)
    {
        infoBox.SetActive(value);
    }

    public void Close()
    {
        infoBox.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
