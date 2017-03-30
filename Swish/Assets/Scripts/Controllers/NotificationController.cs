using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour {

    //Notification Variables
    public GameObject notificationPanel;
    public Text notificationMessage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DismissNotificationPanel()
    {
        notificationPanel.SetActive(false);

    }

    public void NotifyLetterReceived(string word)
    {
        string temp = word.Substring(word.Length - 1);
        notificationPanel.SetActive(true);
        notificationMessage.text = "\t\t\t    Too bad!\n\t\t  You received: '" + temp + "'\nNow, create your own shot!";
    }
}
