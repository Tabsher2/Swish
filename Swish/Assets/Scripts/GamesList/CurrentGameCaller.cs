﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGameCaller : MonoBehaviour {

    public Text winStreak;
    public int user;

    // Use this for initialization
    void Start()
    {
        user = PlayerPrefs.GetInt("userID");
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        winStreak.text = userAccountInfo.winStreak.ToString();
    }
}
