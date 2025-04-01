using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;

public class ChangeWeatherData : MonoBehaviour
{
    public string DataURL; //Get data by link
    public TextMeshProUGUI dateText; //Get UI text
    public TextMeshProUGUI timeText; //Get UI text
    public TextMeshProUGUI temperatureText; //Get UI text
    public TextMeshProUGUI showersText; //Get UI text
    

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
        Debug.Log(obj["current"]["temperature_2m"].Value);//Check can I get temperature
        Debug.Log(obj["current"]["showers"].Value);//Check can I get showers value
        Debug.Log(obj["current"]["time"].Value);//Check can I get current time
        temperatureText.text = obj["current"]["temperature_2m"].Value + "°C";//Put temperature into UI panel
        showersText.text = obj["current"]["showers"].Value + "mm";//Put rainfall into UI panel
        string timeValue = obj["current"]["time"].Value;//Put time into string
        string[] dateAndtime = timeValue.Split('T'); //Split date and time
        if (dateAndtime.Length > 1)
        {   
            //notes[2025-03-11]=0/T/[12:30]=1
            string Today = dateAndtime[0]; 
            string CurrentTime = dateAndtime[1]; 
            Debug.Log(CurrentTime);//Check can I get time
            Debug.Log(Today);//Check can I get date
            dateText.text = Today;//Put date into UI panel
            timeText.text = CurrentTime;//Put time into UI panel
        }
    }
}
