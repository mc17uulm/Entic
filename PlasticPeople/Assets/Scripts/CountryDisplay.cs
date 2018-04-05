using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountryDisplay : MonoBehaviour {

    public GameObject nameText;
    public Image countryImage;
    public GameObject populationText;
    public GameObject codeText;
    public GameObject wasteText;




	// Use this for initialization
	void Start () {
        nameText.GetComponent<TextMeshProUGUI>().text = "TestLand";
    }
}
