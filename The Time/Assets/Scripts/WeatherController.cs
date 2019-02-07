using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class WeatherController : MonoBehaviour {

    private const string API_KEY = "cb163bfeff43ca7f5d7af9bef2db5a32";
    private const float API_CHECK_MAXTIME = 10 * 60.0f; //10 minutes (recomendado por la API)
    private float apiCheckCountdown = API_CHECK_MAXTIME;
    private string cityField = "city.list.json";

    public Text actualWeather;

    public GameObject sun;
    public GameObject snow;
    public GameObject rain;
    public GameObject clear;
    public GameObject standar;


    /*
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(GetWeather(CheckSnowStatus));
    }
	
	// Update is called once per frame
	void Update ()
    {
        apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            apiCheckCountdown = API_CHECK_MAXTIME;
            StartCoroutine(GetWeather(CheckSnowStatus));
        }
    }
    */

    public IEnumerator GetWeather(Action<WeatherInfo> onSuccess, string id)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", id, API_KEY)))
        {
            yield return req.Send();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(weatherJSON);
            onSuccess(info);
        }
    }


    public void CheckSnowStatus(WeatherInfo weatherObj)
    {
        string weather = weatherObj.weather[0].main; //.Equals("Snow");

        if(weather == "Snow")
        {
            snow.SetActive(true);
            standar.SetActive(false);
            rain.SetActive(false);
            clear.SetActive(false);
            sun.SetActive(false);
        }

        if (weather == "Sun")
        {
            snow.SetActive(false);
            standar.SetActive(false);
            rain.SetActive(false);
            clear.SetActive(false);
            sun.SetActive(true);
        }

        if (weather == "Clear")
        {
            snow.SetActive(false);
            standar.SetActive(false);
            rain.SetActive(false);
            clear.SetActive(true);
            sun.SetActive(false);
        }

        if (weather == "Rain")
        {
            snow.SetActive(false);
            standar.SetActive(false);
            rain.SetActive(true);
            clear.SetActive(false);
            sun.SetActive(false);
        }

        if (weather == "Clouds")
        {
            snow.SetActive(false);
            standar.SetActive(false);
            rain.SetActive(false);
            clear.SetActive(true);
            sun.SetActive(false);
        }


        actualWeather.text = weatherObj.weather[0].main.ToString();
    }
}
