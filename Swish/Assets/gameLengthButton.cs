using GameInfoForLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameLengthButton : MonoBehaviour {

    public Button gameLengthButtonItem;
    public GameObject setUpInfo;
    public Text buttonText;

    // ***********This is for the friend list challenge button
    void Start()
    {
        gameLengthButtonItem.onClick.AddListener(TaskOnClick);
        setUpInfo = GameObject.Find("SetUpGameInfo");

    }

    // Update is called once per frame
    void TaskOnClick()
    {
        setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().turnLength = Convert.ToInt32(buttonText.text);
        int user = setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().userID;
        int opponent = setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().opponentID;
        int gameLength = setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().turnLength;
        int gameID = NetworkController.AddGame(user, opponent, gameLength);
        GameObject.Find("GameInfo").GetComponent<GameInfo>().gameID = gameID;
        Debug.Log(GameObject.Find("GameInfo").GetComponent<GameInfo>().gameID + " " + gameID);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
