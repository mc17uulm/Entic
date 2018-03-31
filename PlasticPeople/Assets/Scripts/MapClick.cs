﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class MapClick : MonoBehaviour, IPointerClickHandler {
    
    public GameObject rootElement;
    private string lastClickedCountry = "XX";

    public GameObject nameText;
    public Image countryImage;
    public GameObject populationText;
    public GameObject codeText;
    public GameObject countryInfo;
    public Slider efficiency;

    private Camera camera;
    private Texture2D map;
    private Vector2 cameraDim;
    private Vector2 mapDim;
    private ZoomIn Zoom;

	// Use this for initialization
	void Awake () {
        camera = Camera.main;
        Zoom = GetComponent<ZoomIn>();

        map = Resources.Load("Map/GreyScaleMap2") as Texture2D;

        cameraDim = new Vector2(camera.pixelWidth, camera.pixelHeight);
        mapDim = new Vector2(map.width, map.height);

        //Debug.Log("CameraDim: x=" + cameraDim.x + " y=" + cameraDim.y);
        //Debug.Log("MapDim: x=" + cameraDim.x + " y=" + cameraDim.y);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Mouse clicked");
        //Debug.Log("Scale: " + ZoomIn.scale);
        
        Vector2 pos = eventData.position;

        //Debug.Log("Click pos: " + pos);
        //Debug.Log("MapDim: x=" + mapDim.x + " y=" + mapDim.y);

        float x = (pos.x / cameraDim.x) * mapDim.x;
        float y = (pos.y / cameraDim.y) * mapDim.y;

        //Debug.Log("Absoult click position on image. x=" + x + " y=" + y);

        Color c = map.GetPixel((int)x, (int)y);

        int cint = (int)(c.r * 255) ;

        Debug.Log("Color: " + c.r*255);

        if (cint != 255)
        {
            Logic.Country country = Game.play.GetCountry(cint);
            countryInfo.SetActive(true);

            nameText.GetComponent<TextMeshProUGUI>().text = country.GetName();
            codeText.GetComponent<TextMeshProUGUI>().text = country.GetCode();
            Debug.Log(country.GetPopulation());
            populationText.GetComponent<TextMeshProUGUI>().text = "Population: " + country.GetPopulation();
            Debug.Log(country.GetProduction() * 100);
            efficiency.value = (float)(country.GetProduction() * 100);
            
            Sprite flagTexture = Resources.Load <Sprite> ("Flags/"+ country.GetCode().ToLower());

            if(lastClickedCountry == "XX")
            {
                Debug.Log("First time clicked");
                GameObject clickedCountry = GameObject.Find("/UI/Map/" + country.GetCode());
                clickedCountry.GetComponent<Image>().color = new Color32(152, 152, 152, 255);
                lastClickedCountry = country.GetCode();
            }
            else 
            {
                Debug.Log("Last time you clicked: "+lastClickedCountry);
                Image lastClickedCountryObject = GameObject.Find("/UI/Map/"+lastClickedCountry).GetComponent<Image>();
                lastClickedCountryObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                Debug.Log("The GameObject you clicked was: "+GameObject.Find("/UI/Map/" + country.GetCode()).name);
                Image clickedCountry = GameObject.Find("/UI/Map/" + country.GetCode()).GetComponent<Image>();
                clickedCountry.GetComponent<Image>().color = new Color32(152, 152, 152, 255);
                lastClickedCountry = country.GetCode();
            }
            

            if (flagTexture != null)
            {
                countryImage.sprite = flagTexture;
                countryImage.preserveAspect = true;
            }

            Debug.Log("You clicked : " + country.GetName() + ". They are in trouble now!");
        }
    }



}
