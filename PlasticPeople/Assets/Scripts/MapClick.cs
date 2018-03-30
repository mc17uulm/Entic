using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class MapClick : MonoBehaviour, IPointerClickHandler {

    public string path;
    public string jsonString;
    public Country[] countrylist;

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

        path = Application.streamingAssetsPath + "/countries.json";
        string countries = File.ReadAllText(path);
        countrylist = JsonHelper.FromJson<Country>(countries);
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
            countryInfo.SetActive(true);

            nameText.GetComponent<TextMeshProUGUI>().text = countrylist[cint].name;
            codeText.GetComponent<TextMeshProUGUI>().text = countrylist[cint].code;
            Debug.Log(countrylist[cint].population);
            populationText.GetComponent<TextMeshProUGUI>().text = "Population: "+countrylist[cint].population;
            Debug.Log(countrylist[cint].rate * 100);
            efficiency.value = (float)(countrylist[cint].rate * 100);
            
            Sprite flagTexture = Resources.Load <Sprite> ("Flags/"+countrylist[cint].code.ToLower());

            if (flagTexture != null)
            {
                countryImage.sprite = flagTexture;
                countryImage.preserveAspect = true;
            }

            Debug.Log("You clicked : " + countrylist[cint].name + ". They are in trouble now!");
        }
    }



}
