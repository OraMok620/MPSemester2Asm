using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;

public class ChangeWeather : MonoBehaviour
{
    public string DataURL;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI rainText;
    public Image skyColour;
    

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
        Debug.Log(obj["current"]["temperature_2m"].Value);
        Debug.Log(obj["current"]["rain"].Value);
        Debug.Log(obj["current"]["time"].Value);
        int numOFCubes = obj["current"].Count;
        temperatureText.text = obj["current"]["temperature_2m"].Value + "°C";
        rainText.text = obj["current"]["rain"].Value + "mm";
        string timeValue = obj["current"]["time"].Value; 
        string[] dateAndtime = timeValue.Split('T'); 
        if (dateAndtime.Length > 1)
        {   
            //notes[2025-03-11]=0/T/[12:30]=1
            string Today = dateAndtime[0]; 
            string CurrentTime = dateAndtime[1]; 
            Debug.Log(CurrentTime);
            Debug.Log(Today);
            dateText.text = Today;
            timeText.text = CurrentTime;
            string hourValue = CurrentTime; 
            string[] hourAndminute = hourValue.Split(':');
            //notes[12]=0/:/[45]=1
            if (hourAndminute.Length > 1)
            {
                string Hour = hourAndminute[0];
                Debug.Log(Hour);
                int hour = int.Parse(hourAndminute[0]); 

                if (hour >= 7 && hour <= 18) 
                {
                    skyColour.color = new Color32(21, 204, 240, 100);
                    Debug.Log("DayTime");
                }
                else 
                {
                    skyColour.color = new Color32(10, 10, 50, 100);
                    Debug.Log("NightTime");
                }
            }
        }
    }
}
