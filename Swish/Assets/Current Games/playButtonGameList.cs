using gameListInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameInfoForLoad;
using gameListInfo;

public class playButtonGameList : MonoBehaviour {
    public GameObject thisObject;
    public Button playButtonListItem;

    void Start()
    {
        Button btn = playButtonListItem.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        GameObject.Find("GameInfo").GetComponent<GameInfo>().gameID = thisObject.GetComponent<currentGameContentInfo>().gameID;
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
