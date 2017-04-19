using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppLogIn : MonoBehaviour {

    public Text email;
    public Text password;
    public Button logIn;
    public bool loggedIn;
    private int userID;

    private void Start()
    {
        logIn.onClick.AddListener(loginClicked);
    }

    void loginClicked()
    {
        userID = NetworkController.Login(email.text, password.text);

        if(userID == -111)
        {
            Debug.Log("Add wrong email or id");
        }
        else
        {
            PlayerPrefs.SetInt("userID", userID);
            Debug.Log(PlayerPrefs.GetInt("userID"));
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

    }
}
