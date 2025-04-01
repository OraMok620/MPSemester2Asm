using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class SunLight : MonoBehaviour
{
    public string DataURL; //GetData
    public Light sunLight;//GetLight
    public GameObject sun;//GetSun
    

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
        
        int isDay = node["current"]["is_day"].AsInt; // Change is_day value into integer

        if (isDay == 1)
        {
            sunLight.enabled = true;  // Sun come out
            sun.SetActive(true);
            Debug.Log("DayTime");
        }
        else
        {
            sunLight.enabled = false; // Bye Sun
            sun.SetActive(false);
            Debug.Log("NightTime");
        }
    }
}
