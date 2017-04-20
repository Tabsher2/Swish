using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addFriendButton : MonoBehaviour {

    public Button playButtonListItem;
    public GameObject addPanel;

    void Start()
    {
        Button btn = playButtonListItem.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("clicked add");
        addPanel.SetActive(true);
    }

}
