using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System;

public class ReadJson : MonoBehaviour {

    private string FILE_PATH = "/StreamingAssets/city.json";

    private JsonData itemData;
    private string id;


    public Text debug;
    public Text debug2;
    public Text debug3;
    private string path;
    private string json;

    public string searchCity(string city, string country)
    {
        //jsonString = File.ReadAllText(Application.dataPath + FILE_PATH);

        path = "jar:file://" + Application.dataPath + "!/assets/city.json";
        WWW wwwfile = new WWW(path);
        while (!wwwfile.isDone) { }
        json = wwwfile.text;
        itemData = JsonMapper.ToObject(json);
        //itemData = JsonMapper.ToObject(jsonString);
        id = (GetItem(city, country)["id"]).ToString();

        return id;
    }

    JsonData GetItem(string city, string country)
    {
        for(int i = 0; i < itemData.Count; i++)
        {
            if (itemData[i]["name"].ToString().ToLower() == city.ToLower() && itemData[i]["country"].ToString().ToLower() == country.ToLower())
                return itemData[i];
        }

        return null;
    }

}
