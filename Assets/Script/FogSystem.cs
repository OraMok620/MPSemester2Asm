using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class FogSystem : MonoBehaviour
{
    public string DataURL; //Get data
    public GameObject Fog;//Get object of Fog
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(DataURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log(json);
                ReadJSON(json);
            }
        }
    }

    void ReadJSON(string jsonString)
    {
        JSONNode node = JSON.Parse(jsonString);
        JSONObject obj = node.AsObject;

        Debug.Log(obj["current"]["weather_code"].Value);//Check can I get weather code
        int currentWeatherCode = node["current"]["weather_code"].AsInt; //Put value into integer

        if (currentWeatherCode >= 40 && currentWeatherCode <= 49)
        {
            Fog.SetActive(true); //if humidity larger than 95% there are fog
            Debug.Log("High humidity"); //put to console to check is the code work
        }
        else
        {
            Fog.SetActive(false); //else no
            Debug.Log("No Fog"); //same for check
        }
    }
}
