using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playFriendButton : MonoBehaviour

    //********This is only for the set up game play friend button, not the challenge button on the friend list prefab

{
    public Button playFriendButtonItem;
    public GameObject scrollView;
    // Use this for initialization
    void Start ()
    {
        Button btn = playFriendButtonItem.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
	
	void TaskOnClick()
    {
        scrollView.SetActive(true);
    }
	
}
