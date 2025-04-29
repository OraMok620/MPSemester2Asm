using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class ThunderSystem : MonoBehaviour
{
 public string DataURL; //Get data
    public GameObject Thunder;//Get object of Thunder
    

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

        if(currentWeatherCode == 17 || currentWeatherCode == 29 || currentWeatherCode >= 95)
        {
            Thunder.SetActive(true);
            Debug.Log("ThunderNow");//Check if the if statement work by call the message in console
        } else{
            Thunder.SetActive(false);
            Debug.Log("NoThunder");//Check if the if statement work by call the message in console
        }
    }
}
