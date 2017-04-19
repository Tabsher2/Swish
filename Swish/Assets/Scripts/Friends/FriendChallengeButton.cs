using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using setUpGameInformantion;
using activate;
public class FriendChallengeButton : MonoBehaviour {

    public Button challengeButton;
    public GameObject setUpInfo;
    public GameObject thisObject;
    public GameObject selectLength;

	// ***********This is for the friend list challenge button
	void Start () {
        challengeButton.onClick.AddListener(TaskOnClick);
        setUpInfo = GameObject.Find("SetUpGameInfo");

    }
	
	// Update is called once per frame
	void TaskOnClick () {
		setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().opponentID = thisObject.GetComponent<friendsListInfo.friendListPlayerID>().friendIDname;
        
    }
}
