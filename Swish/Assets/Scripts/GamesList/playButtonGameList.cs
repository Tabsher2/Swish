using gameListInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameInfoForLoad;
 

public class playButtonGameList : MonoBehaviour {
    public GameObject thisObject;
    public Button playButtonListItem;
    public int user;
    public int turnOwner;
    void Start()
    {
        user = PlayerPrefs.GetInt("userID");
        turnOwner = thisObject.GetComponent<currentGameContentInfo>().turn;
        Button btn = playButtonListItem.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        if(turnOwner != user)
        {
            btn.gameObject.SetActive(false);
        }
    }

    void TaskOnClick()
    {

        GameObject.Find("GameInfo").GetComponent<GameInfo>().gameID = thisObject.GetComponent<currentGameContentInfo>().gameID;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
