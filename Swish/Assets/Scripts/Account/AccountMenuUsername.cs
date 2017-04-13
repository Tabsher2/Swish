﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkData;
using UnityEngine.UI;

public class AccountMenuUsername : MonoBehaviour {

    public Text username;
    private int user = 4;

    // Use this for initialization
    void Start () {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        username.text = userAccountInfo.userName;

    }
	
}
