using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInSignUpController : MonoBehaviour {

    public GameObject signUpPanel;
    public GameObject loginPanel;

    void Start () {
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }
	
}
