using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeButton : MonoBehaviour {

    public Button challengeButton;
    public GameObject setLengthPanel;
    public GameObject setUpInfo;
    private bool flag;
	// Use this for initialization
	void Start () {
        challengeButton.onClick.AddListener(TaskOnClick);
        setUpInfo = GameObject.Find("SetUpGameInfo");
    }
    private void Update()
    {
        if(flag == true && setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().turnLength != 0)
        {
            setLengthPanel.SetActive(false);
            flag = false;
        }
    }

    void TaskOnClick()
    {
        setUpInfo.GetComponent<setUpGameInformantion.setUpGameInfo>().opponentID = NetworkController.FindOpponent(PlayerPrefs.GetInt("userID"));
        setLengthPanel.SetActive(true);
        flag = true;
    }

}
