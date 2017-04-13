﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuSwishesMade : MonoBehaviour {

    public Text swishesMade;
    private int user = 4;

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        swishesMade.text = userAccountInfo.swishesMade.ToString();
    }
}
