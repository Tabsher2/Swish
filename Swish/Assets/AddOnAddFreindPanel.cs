using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddOnAddFreindPanel : MonoBehaviour {

    public Button addButton;
    public GameObject inputBox;
    public GameObject addPanel;
    public GameObject addUserDoesNotExistPanel;

    // Use this for initialization
    void Start () {
        Button btn = addButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void TaskOnClick()
    {
        Debug.Log(inputBox.GetComponent<InputField>().text);
        int friendID = NetworkController.GetUserID(inputBox.GetComponent<InputField>().text);

        if(friendID == 0)
        {
            addUserDoesNotExistPanel.SetActive(true);
            addPanel.SetActive(false);
            return;
        }
        Debug.Log("friendID: " + friendID);
        NetworkController.AddFriend(PlayerPrefs.GetInt("userID"), friendID);
        addPanel.SetActive(false);
    }
}
