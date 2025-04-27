using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class RainSystem : MonoBehaviour
{
    public string DataURL; //Get data
    public GameObject lightRain;//Get object of lightRain
    public GameObject rain;//Get object of rain
    public GameObject heavyRain;//Get object of heavyRain
    

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

        Debug.Log(obj["current"]["rain"].Value);//Check can I get showers value
        int RainAmount = node["current"]["rain"].AsInt; //Put value into integer

        if(RainAmount >= 4)
        {
            heavyRain.SetActive(true);
            Debug.Log("HeavyRainNow");//Check if the if statement work by call the message in console
        } else{
            heavyRain.SetActive(false);
            Debug.Log("NotHeavyRain");//Check if the if statement work by call the message in console
        }
        if(RainAmount >= 0.5 && RainAmount < 4)
        {
            rain.SetActive(true);
            Debug.Log("RainNow");//Check if the if statement work by call the message in console
        } else{
            rain.SetActive(false);
            Debug.Log("NotRain");//Check if the if statement work by call the message in console
        }
        if(RainAmount > 0 && RainAmount < 0.5)
        {
            lightRain.SetActive(true);
            Debug.Log("LightRainNow");//Check if the if statement work by call the message in console
        } else{
            lightRain.SetActive(false);
            Debug.Log("NoRain");//Check if the if statement work by call the message in console
        }
    }
}
