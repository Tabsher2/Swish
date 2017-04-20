using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToLoginOnCancel : MonoBehaviour {

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
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }
}
