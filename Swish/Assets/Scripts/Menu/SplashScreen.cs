using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {;
        yield return new WaitForSecondsRealtime(2);
        if (PlayerPrefs.GetInt("user") <= 0)
        {
            SceneManager.LoadScene("Login", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
