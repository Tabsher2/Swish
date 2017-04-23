using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToLoginOnCancel : MonoBehaviour {

    public InputField emailField;
    public InputField usernameField;
    public InputField pwField;
    public InputField dobField;
    public Text errorText;

    public Button ButtonCancel;

    public GameObject signUpPanel;
    public GameObject loginPanel;
    // Use this for initialization
    void Start()
    {
        ButtonCancel.onClick.AddListener(cancelClicked);
    }

    void cancelClicked()
    {
        emailField.text = "";
        usernameField.text = "";
        pwField.text = "";
        dobField.text = "";
        errorText.text = "";
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }
}
