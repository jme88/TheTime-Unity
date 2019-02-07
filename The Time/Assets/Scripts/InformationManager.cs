using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationManager : MonoBehaviour {

    public InputField inputCountry;
    public InputField inputCity;
    public ReadJson readJson;
    public WeatherController weatherController;

    private string country;
    private string city;
    private string id;


    public Text debug;
    public Text debug2;
    public Text debug3;

    public void inputInformationClick()
    {
        debug.text = "Helo";
        country = inputCountry.text;
        city = inputCity.text;
        if (country != null && city != null)
        {
            id = readJson.searchCity(city, country);
            StartCoroutine(weatherController.GetWeather(weatherController.CheckSnowStatus, id));
        }
    }
}
