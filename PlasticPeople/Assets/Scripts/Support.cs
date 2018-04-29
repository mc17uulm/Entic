using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Support : MonoBehaviour,  IPointerClickHandler{

    public void OnPointerClick(PointerEventData data)
    {
        Game.play.Support();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
