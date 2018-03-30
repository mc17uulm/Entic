using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawMap : MonoBehaviour {


    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject ParentPanel;
    // Use this for initialization
    void Awake () {
        foreach (Sprite currentSprite in Sprites)
        {
            Debug.Log("Sprite "+currentSprite.name);
            GameObject NewObj = new GameObject(); //Create the GameObject
            Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
            NewImage.sprite = currentSprite; //Set the Sprite of the Image Component on the new GameObject
            NewImage.transform.localScale = ParentPanel.transform.localScale;
            NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
            NewObj.name = currentSprite.name;
            NewObj.SetActive(true); //Activate the GameObject
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
