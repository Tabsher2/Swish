﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuCurrentStreak : MonoBehaviour {

    public Text winStreak;
    public int user = 601;

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        winStreak.text = userAccountInfo.winStreak;
    }
}
