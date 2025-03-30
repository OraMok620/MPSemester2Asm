using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class sky_change : MonoBehaviour
{
    public string DataURL;
    public Material targetMaterial;
    

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
        Debug.Log(obj["current"]["time"].Value);
        string timeValue = obj["current"]["time"].Value; 
        string[] dateAndtime = timeValue.Split('T'); 
        if (dateAndtime.Length > 1)
        {   
            //notes[2025-03-11]=0/T/[12:30]=1
            string Today = dateAndtime[0]; 
            string CurrentTime = dateAndtime[1]; 
            string hourValue = CurrentTime; 
            string[] hourAndminute = hourValue.Split(':');
            //notes[12]=0/:/[45]=1
            if (hourAndminute.Length > 1)
            {
                string Hour = hourAndminute[0];
                Debug.Log(Hour);
                int hour = int.Parse(hourAndminute[0]); 

                if (hour >= 6 && hour <= 18) 
                {
                    Debug.Log("DayTime");
                    targetMaterial.color = new Color32(181, 247, 255, 255);
                }
                else 
                {
                    Debug.Log("NightTime");
                    targetMaterial.color = new Color32(0, 47, 105, 255);
                }
            }
        }
    }
}

