using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuLeaderBoardDisplay : MonoBehaviour {

    public Text leaderTable;
    string tableInfo;
    string[] names = new string[10];
    int[] wins = new int[10];

	// Use this for initialization
	void Start () {

        for( int  i = 0; i < 10; i++)
        {
            //retrieve users in rank order from database and store here
            names[i] = "user" + (i+1);
        }

        for (int i = 0; i < 10; i++)
        {
            //retrieve each users wins and store here
        }

        tableInfo = "Rank\t\t\t" + "User\t\t\t" + "Wins\n";
        for (int i = 0; i < 10; i++)
        {
            tableInfo += (i+1) + "\t\t\t" +  names[i] + "\t\t\t" + wins[i] + "\n";
        }
        leaderTable.text = tableInfo;
	}
	
}
