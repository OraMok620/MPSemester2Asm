 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private bool isMain = true; // Track which scene is currently active

    public void OnButtonPress()
    {
        if (isMain)
        {
            SceneManager.LoadScene("Filter");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }

        isMain = !isMain; // Toggle the scene tracker
    }
}
