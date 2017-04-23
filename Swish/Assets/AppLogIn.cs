using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppLogIn : MonoBehaviour {

    public GameObject email;
    public GameObject password;
    public GameObject accountCreated;
    public GameObject error;
    public Button logIn;
    public bool loggedIn;
    private int userID;

    private void Start()
    {
        logIn.onClick.AddListener(loginClicked);
    }

    void loginClicked()
    {
        Debug.Log(email.GetComponent<InputField>().text);
        userID = NetworkController.Login(email.GetComponent<InputField>().text, password.GetComponent<InputField>().text);

        if(userID == -111)
        {
            Debug.Log("Add wrong email or id");
            accountCreated.SetActive(false);
            error.SetActive(true);
        }
        else if(userID == -1)
        {
            Debug.Log("wrong password or username");
            accountCreated.SetActive(false);
            error.SetActive(true);
        }
        else
        {
            accountCreated.SetActive(false);
            error.SetActive(false);
            PlayerPrefs.SetInt("userID", userID);
            Debug.Log(PlayerPrefs.GetInt("userID"));
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

    }
}
