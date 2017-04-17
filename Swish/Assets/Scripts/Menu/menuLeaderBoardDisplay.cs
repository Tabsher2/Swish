using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuLeaderBoardDisplay : MonoBehaviour
{

    public Text leader1;
    public Text leader2;
    public Text leader3;
    public Text leader4;
    public Text leader5;
    public Text leader6;
    public Text leader7;
    public Text leader8;
    public Text leader9;
    public Text leader10;
    public Text leaderColumns;

    public Text leaderWins1;
    public Text leaderWins2;
    public Text leaderWins3;
    public Text leaderWins4;
    public Text leaderWins5;
    public Text leaderWins6;
    public Text leaderWins7;
    public Text leaderWins8;
    public Text leaderWins9;
    public Text leaderWins10;

    string tableInfo;
    string[] names = new string[10];
    int[] wins = new int[10];

    // Use this for initialization
    void Start()
    {

        List<NetworkData.statLeaderName> statLeaders = NetworkController.getLeaderboard("totalWins");
        List<NetworkData.statLeaderWins> statLeaderWins = NetworkController.getLeaderboardWins("totalWins");
        //  Debug.Log(statLeaderWins[0].statLeaderWinNum);
        for (int i = 0; i < 10; i++)
        {
            //retrieve users in rank order from database and store here
            names[i] = statLeaders[i].statLeader;
        }

        //get user wins
        for (int i = 0; i < 10; i++)
        {
            //retrieve users wins in rank order from database and store here
            wins[i] = statLeaderWins[i].statLeader;

        }

        for (int i = 0; i < 10; i++)
        {
            //retrieve each users wins and store here
        }

        leader1.text = names[0];
        leader2.text = names[1];
        leader3.text = names[2];
        leader4.text = names[3];
        leader5.text = names[4];
        leader6.text = names[5];
        leader7.text = names[6];
        leader8.text = names[7];
        leader9.text = names[8];
        leader10.text = names[9];

        leaderWins1.text = Convert.ToString(wins[0]);
        leaderWins2.text = Convert.ToString(wins[1]);
        leaderWins3.text = Convert.ToString(wins[2]);
        leaderWins4.text = Convert.ToString(wins[3]);
        leaderWins5.text = Convert.ToString(wins[4]);
        leaderWins6.text = Convert.ToString(wins[5]);
        leaderWins7.text = Convert.ToString(wins[6]);
        leaderWins8.text = Convert.ToString(wins[7]);
        leaderWins9.text = Convert.ToString(wins[8]);
        leaderWins10.text = Convert.ToString(wins[9]);
    }

}
