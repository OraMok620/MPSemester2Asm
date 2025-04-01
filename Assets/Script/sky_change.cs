using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class sky_change : MonoBehaviour
{
    public string DataURL; //GetData
    public Material targetMaterial; //Get Material
    

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

        Debug.Log(obj["current"]["is_day"].Value);//Check is_day value
        int isDay = node["current"]["is_day"].AsInt; // Change is_day value into integer
        Debug.Log(obj["current"]["showers"].Value);//Check can I get showers value
        int RainAmount = node["current"]["showers"].AsInt; //Put value into integer

        if (isDay == 1)
        {
            Debug.Log("DayTime");
            targetMaterial.color = new Color32(97, 189, 252, 255);
        }
        else
        {
            Debug.Log("NightTime");
            targetMaterial.color = new Color32(0, 23, 50, 255);
        }
        if (isDay == 1 && RainAmount > 0) 
        {
            Debug.Log("IsRaining");
            targetMaterial.color = new Color32(160, 160, 160, 255);
        }
        
    }
}

