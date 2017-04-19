using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToSignUp : MonoBehaviour {

    public Button ButtonGoToSignUp;

    public GameObject signUpPanel;
    public GameObject loginPanel;
    // Use this for initialization
    void Start () {
        ButtonGoToSignUp.onClick.AddListener(goToSignUpClicked);
    }

    void goToSignUpClicked()
    {
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
	
}
