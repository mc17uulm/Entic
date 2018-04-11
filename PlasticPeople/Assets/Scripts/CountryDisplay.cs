using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CountryDisplay : MonoBehaviour {

    public TextMeshProUGUI nameText;
    public Image countryImage;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI codeText;
    public Slider efficiency;
    public TextMeshProUGUI wasteText;
    public TextMeshProUGUI rateText;
    public GameObject countryInfo;
    private string lastClickedCountry = "XX";
    private float lastDensity = 1.0f;

    public void ShowClickedCountry(int id)
    {
        Debug.Log("ID: " + id);
        if (id != 255)
        {
            Logic.Country country = Game.play.GetCountry(id);

            int tmp = (int) (255 * country.GetDensity());
            Debug.Log("TMP: " + tmp);
            byte density = (byte) tmp;
            Debug.Log("Byte: " + density);

            Debug.Log("Density: " + country.GetDensity());

            nameText.text = country.GetName();
            codeText.text = country.GetCode();
            populationText.text = "Population: " + country.PrintPopulation();
            efficiency.value = (float)(country.GetProduction() / country.GetPopulation() / 0.0337f * 50);
            wasteText.text = country.PrintWaste();
            rateText.text = country.PrintProduction();

            Sprite flagTexture = Resources.Load<Sprite>("Flags/" + country.GetCode().ToLower());

            if (lastClickedCountry == "XX")
            {
                //Debug.Log("First time clicked");
                GameObject clickedCountry = GameObject.Find("/UI/Map/" + country.GetCode());
                clickedCountry.GetComponent<Image>().color = new Color(0.5960785f, 0.5960785f, 0.5960785f, country.GetDensity());
                lastClickedCountry = country.GetCode();
                lastDensity = country.GetDensity();
            }
            else
            {
                //Debug.Log("Last time you clicked: "+lastClickedCountry);
                Image lastClickedCountryObject = GameObject.Find("/UI/Map/" + lastClickedCountry).GetComponent<Image>();
                Color c = lastClickedCountryObject.GetComponent<Image>().color;
                lastClickedCountryObject.GetComponent<Image>().color = new Color(1, 1, 1, lastDensity);
                //Debug.Log("The GameObject you clicked was: "+GameObject.Find("/UI/Map/" + country.GetCode()).name);
                Image clickedCountry = GameObject.Find("/UI/Map/" + country.GetCode()).GetComponent<Image>();
                clickedCountry.GetComponent<Image>().color = new Color(0.5960785f, 0.5960785f, 0.5960785f, country.GetDensity());
                lastClickedCountry = country.GetCode();
                lastDensity = country.GetDensity();
            }


            if (flagTexture != null)
            {
                countryImage.sprite = flagTexture;
                countryImage.preserveAspect = true;
            }
            countryInfo.SetActive(true);
        }
        else
        {
            if (lastClickedCountry == "XX")
            {
                countryInfo.SetActive(false);
            }
            else
            {
                HideCountryInfo();
            }
        }

    }

    public void HideCountryInfo()
    {
        countryInfo.SetActive(false);
        Image lastClickedCountryObject = GameObject.Find("/UI/Map/" + lastClickedCountry).GetComponent<Image>();
        lastClickedCountryObject.GetComponent<Image>().color = new Color(1, 1, 1, lastDensity);
        lastClickedCountry = "XX";
    }
}
