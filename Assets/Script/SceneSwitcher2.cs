using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher2 : MonoBehaviour
{
    private bool isFilter = true; // Track which scene is currently active

    public void OnButtonPress()
    {
        if (isFilter)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            SceneManager.LoadScene("Filter");
        }

        isFilter = !isFilter; // Toggle the scene tracker
    }
}
