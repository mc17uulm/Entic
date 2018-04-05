using UnityEngine;

public class ZoomIn : MonoBehaviour
{

    private GameObject map;
    private float factor;
    public static Vector3 scale;

    // Use this for initialization
    void Start()
    {
        factor = 1.05f;
        Debug.Log("Scale: " + transform.localScale);
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            transform.localScale *= factor;
            if (transform.localScale.x > 10)
            {
                transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
            }
            scale = transform.localScale;
            Debug.Log("Scale Now: " + transform.localScale);
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            transform.localScale /= factor;
            if (transform.localScale.x < 1)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            scale = transform.localScale;
            Debug.Log("Scale Now: " + transform.localScale);
        }
    }

    public Vector3 getScale()
    {
        return scale;
    }
}