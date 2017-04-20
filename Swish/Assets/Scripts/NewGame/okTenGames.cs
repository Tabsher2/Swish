using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class okTenGames : MonoBehaviour {

    public Button okButton;

    public GameObject tenGamesPanel;

    // ***********This is for the friend list challenge button
    void Start()
    {
        okButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        tenGamesPanel.SetActive(false);
    }
}
