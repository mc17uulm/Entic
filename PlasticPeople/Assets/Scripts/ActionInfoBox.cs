using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionInfoBox : MonoBehaviour {

    public TextMeshProUGUI header;
    public TextMeshProUGUI descr;
    public TextMeshProUGUI info;
    public GameObject infoBox;

    public void SetActive(bool value)
    {
        infoBox.SetActive(value);
    }

    public void Close()
    {
        infoBox.SetActive(false);
    }

    public void ChangeHeader(string text)
    {
        header.text = text;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
