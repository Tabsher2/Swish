using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonLogout : MonoBehaviour {

    public Button btnLogout;
	// Use this for initialization
	void Start () {
        Button btn = btnLogout.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void TaskOnClick () {
        Debug.Log("CLICKED");
        PlayerPrefs.SetInt("userID", 0);
        SceneManager.LoadScene("Login", LoadSceneMode.Single);
    }
}
