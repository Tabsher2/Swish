using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NetworkData;
using System;


namespace account
{

    public class userAccountInfo : MonoBehaviour
    {
        public Text userName;
        public Text userEmail;
        public Text gamesWon;
        public Text swishesMade;
        public Text winStreak;
        public Text totalScore;
        public Text bestWinStreak;
        public Text winRatio;
        public Text avgScorePerGame;
        public Text bestSingleShot;
        public Text bestShotStreak;
        public Text currentShotStreak;
        // Use this for initialization
        void Start()
        {

            setData();

        }

        // Update is called once per frame
        public void setData()
        {
            NetworkData.AccountInfo userAccount = NetworkController.FetchAccountInfo(PlayerPrefs.GetInt("userID"));

            userName.text = userAccount.userName;
            userEmail.text = userAccount.userEmail;
            gamesWon.text = Convert.ToString(userAccount.gamesWon);
            swishesMade.text = Convert.ToString(userAccount.swishesMade);
            winStreak.text = Convert.ToString(userAccount.winStreak);
            totalScore.text = userAccount.totalScore;
            bestWinStreak.text = userAccount.bestWinStreak;
            winRatio.text = userAccount.winRatio;
            avgScorePerGame.text = userAccount.avgScorePerGame;
            bestSingleShot.text = userAccount.bestSingleShot;
            bestShotStreak.text = userAccount.bestShotStreak;
            currentShotStreak.text = userAccount.currentShotStreak;
        }
    }
}
