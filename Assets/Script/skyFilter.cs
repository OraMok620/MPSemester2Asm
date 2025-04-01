using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;

public class skyFilter : MonoBehaviour
{
    public string DataURL; //GetData
    public GameObject filter;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
        Image filterImage = filter.GetComponent<Image>();
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
        Image filterImage = filter.GetComponent<Image>();

        if (isDay == 1)
        {
            Debug.Log("DayTime");
            filterImage.color = new Color32(138, 232, 255, 150);
        }
        else
        {
            Debug.Log("NightTime");
            filterImage.color = new Color32(18, 26, 68, 150);
        }
        if (isDay == 1 && RainAmount > 0) 
        {
            Debug.Log("IsRaining");
            filterImage.color = new Color32(150, 150, 150, 150);
        }
        
    }
}
