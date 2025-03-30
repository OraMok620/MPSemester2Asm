using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class Cloud1 : MonoBehaviour
{
    public string DataURL;
    public GameObject over70cloud;
    

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

        Debug.Log(obj["current"]["cloud_cover"].Value);
        int NumOfCloud = int.Parse(obj["current"]["cloud_cover"].Value);
        if (NumOfCloud >= 70)
        {
            over70cloud.SetActive(true);
            Debug.Log("CloudCoverOver70");
        } else {
            over70cloud.SetActive(false);
            Debug.Log("CloudCoverLower70");
        }
    }
}

