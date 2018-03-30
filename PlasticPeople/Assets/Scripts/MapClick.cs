using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapClick : MonoBehaviour, IPointerClickHandler {


    private Camera camera;
    private Texture2D map;
    private Vector2 cameraDim;
    private Vector2 mapDim;
    private ZoomIn Zoom;

	// Use this for initialization
	void Awake () {
        camera = Camera.main;
        Zoom = GetComponent<ZoomIn>();

        map = Resources.Load("Map/GreyScaleMap") as Texture2D;

        cameraDim = new Vector2(camera.pixelWidth, camera.pixelHeight);
        mapDim = new Vector2(map.width, map.height);

        Debug.Log("CameraDim: x=" + cameraDim.x + " y=" + cameraDim.y);
        Debug.Log("MapDim: x=" + cameraDim.x + " y=" + cameraDim.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Mouse clicked");
        Debug.Log("Scale: " + ZoomIn.scale);
        
        Vector2 pos = eventData.position;

        Debug.Log("Click pos: " + pos);
        Debug.Log("MapDim: x=" + mapDim.x + " y=" + mapDim.y);

        float x = (pos.x / cameraDim.x) * mapDim.x;
        float y = (pos.y / cameraDim.y) * mapDim.y;

        Debug.Log("Absoult click position on image. x=" + x + " y=" + y);

        Color c = map.GetPixel((int)x, (int)y);

        Debug.Log("Color: " + c.r*255);
    }



}
