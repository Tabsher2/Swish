using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuCurrentStreak : MonoBehaviour {

    public Text winStreak;
    public string user = "/liCWn1QSAx8RCfPOI+c42aXrD7tW3DyoEVXLiV+IXg=";

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        winStreak.text = userAccountInfo.winStreak;
    }
}
