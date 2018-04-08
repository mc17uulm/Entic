using System.Collections;
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
    public GameObject wasteText;
    public GameObject countryInfo;
    public Slider efficiency;

    private Camera camera;
    private Vector2 pos;
    private Vector2 infoPos;
    private Texture2D map;
    private Vector2 cameraDim;
    private Vector2 mapDim;
    private ZoomIn Zoom;

	// Use this for initialization
	void Awake () {
        camera = Camera.main;
        Zoom = GetComponent<ZoomIn>();

        map = Resources.Load("Map/background") as Texture2D;

        cameraDim = new Vector2(camera.pixelWidth, camera.pixelHeight);
        mapDim = new Vector2(map.width, map.height);

        //Debug.Log("CameraDim: x=" + cameraDim.x + " y=" + cameraDim.y);
        //Debug.Log("MapDim: x=" + mapDim.x + " y=" + mapDim.y);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Mouse clicked");
        //Debug.Log("Scale: " + ZoomIn.scale);
        
        pos = eventData.position;

        infoPos = Camera.main.ScreenToWorldPoint(eventData.position);
        //Debug.Log("Click pos: " + pos);
        //Debug.Log("MapDim: x=" + mapDim.x + " y=" + mapDim.y);

        float x = (pos.x / cameraDim.x) * mapDim.x;
        float y = (pos.y / cameraDim.y) * mapDim.y;

        //Debug.Log("clicked position: "+pos.x+","+pos.y);
        //Debug.Log("InfoPanelPosLocalPosition: "+countryInfo.GetComponent<RectTransform>().localPosition.x+", "+countryInfo.GetComponent<RectTransform>().localPosition.y);
        
        PositionCountryInfo(x, y);
        
        //Debug.Log("Absoult click position on image. x=" + x + " y=" + y);

        Color c = map.GetPixel((int)x, (int)y);

        int cint = (int)(c.r * 255) ;

        //Debug.Log("Color: " + c.r*255);

        CountryDisplay display = FindObjectOfType<CountryDisplay>();
        display.ShowClickedCountry(cint);
    }

    public void PositionCountryInfo(float x, float y)
    {
        
        if (pos.x <= cameraDim.x/2 && pos.y > mapDim.y/2)
        {
            //Debug.Log("1. Quadrant");
            countryInfo.GetComponent<RectTransform>().pivot = new Vector2(0f,1f);
            countryInfo.GetComponent<RectTransform>().position = infoPos;

        }
        else if(pos.x > cameraDim.x / 2 && pos.y > mapDim.y / 2)
        {
            //Debug.Log("2. Quadrant");
            countryInfo.GetComponent<RectTransform>().pivot = new Vector2(1f, 1f);
            countryInfo.GetComponent<RectTransform>().position = infoPos;
        }
        else if(pos.x > cameraDim.x / 2 && pos.y <= mapDim.y / 2)
        {
            //Debug.Log("3. Quadrant");
            countryInfo.GetComponent<RectTransform>().pivot = new Vector2(1f, 0f);
            countryInfo.GetComponent<RectTransform>().position = infoPos;
        }
        else
        {
            //Debug.Log("4. Quadrant");
            countryInfo.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
            countryInfo.GetComponent<RectTransform>().position = infoPos;
        }
    }



}
