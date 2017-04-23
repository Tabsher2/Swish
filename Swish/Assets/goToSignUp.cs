using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToSignUp : MonoBehaviour {

    public InputField emailField;
    public InputField pwField;

    public Button ButtonGoToSignUp;

    public GameObject accountCreated;
    public GameObject error;
    public GameObject signUpPanel;
    public GameObject loginPanel;
    // Use this for initialization
    void Start () {
        ButtonGoToSignUp.onClick.AddListener(goToSignUpClicked);
    }

    void goToSignUpClicked()
    {
        emailField.text = "";
        pwField.text = "";
        accountCreated.SetActive(false);
        error.SetActive(false);
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
	
}
