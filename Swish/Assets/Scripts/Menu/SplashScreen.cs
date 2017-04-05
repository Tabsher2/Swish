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
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
