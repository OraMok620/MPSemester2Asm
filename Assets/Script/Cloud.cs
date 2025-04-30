using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class Cloud : MonoBehaviour
{
    public string DataURL; //Get data
    public GameObject over75cloud;//Get object of cloud cover over 75
    public GameObject over25cloud;//Get object of cloud cover over 25
    public GameObject over0cloud;//Get object of cloud cover over 0
    public Material targetMaterial1; //Get Cloud_cumulus color
    public Material targetMaterial2; //Get Clouds_cumulus_puffy color
    public Material targetMaterial3; //Get Clouds_Strato color
    

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

        Debug.Log(obj["current"]["cloud_cover"].Value);//Check can I get cloud cover value
        int NumOfCloud = int.Parse(obj["current"]["cloud_cover"].Value);//Set the value to integer
        Debug.Log(obj["current"]["rain"].Value);//Check can I get showers value
        int RainAmount = node["current"]["rain"].AsInt; //Put value into integer
        Debug.Log(obj["current"]["weather_code"].Value);//Check can I get weather code
        int currentWeatherCode = node["current"]["weather_code"].AsInt; //Put value into integer

        if (NumOfCloud >= 75) //If cloud cover over 75 than call out the object, otherwise, won't appear
        {
            over75cloud.SetActive(true);
            Debug.Log("CloudCoverOver75");//Check if the if statement work by call the message in console
        } else {
            over75cloud.SetActive(false);
            Debug.Log("CloudCoverLower75");
        }
        if (NumOfCloud >= 25 && NumOfCloud < 75)
        {
            over25cloud.SetActive(true);
            Debug.Log("CloudCoverOver25");//Check if the if statement work by call the message in console
        } else {
            over25cloud.SetActive(false);
            Debug.Log("CloudCoverLower25ORLarger75");
        }
        if (NumOfCloud > 0 && NumOfCloud < 25)
        {
            over0cloud.SetActive(true);
            Debug.Log("CloudCoverOver0");//Check if the if statement work by call the message in console
        } else {
            over0cloud.SetActive(false);
            Debug.Log("NoCloudORLarger25");
        }
        if (RainAmount == 0)
        {
            Debug.Log("IsRaining");
            targetMaterial1.color = new Color32(220, 220, 220, 255); //Change clouds color from white to grey
            targetMaterial2.color = new Color32(220, 220, 220, 255);
            targetMaterial3.color = new Color32(220, 220, 220, 255);
        }
        
        if(currentWeatherCode >= 28 && currentWeatherCode < 31 )
        {
            targetMaterial1.color = new Color32(220, 220, 220, 255); //Change clouds color from white to grey
            targetMaterial2.color = new Color32(220, 220, 220, 255);
            targetMaterial3.color = new Color32(220, 220, 220, 255);
            Debug.Log("ThunderNow");//Check if the if statement work by call the message in console
        } 
    }
}

