﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGameCaller : MonoBehaviour {

    public Text winStreak;
    public int user = 4;

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        winStreak.text = userAccountInfo.winStreak.ToString();
    }
}