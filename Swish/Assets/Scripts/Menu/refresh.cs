using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GCmenu;
using leaderboard;

public class refresh : MonoBehaviour
{

    public Button refreshBtn;

    // Use this for initialization
    void Start()
    {
        Button btn = refreshBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        GameControllerMenu.refresh = true;
        Debug.Log("refresh");
    }
}
