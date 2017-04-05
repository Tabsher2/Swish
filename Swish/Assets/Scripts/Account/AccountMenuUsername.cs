using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkData;
using UnityEngine.UI;

public class AccountMenuUsername : MonoBehaviour {

    public Text username;
    public string user = "/liCWn1QSAx8RCfPOI+c42aXrD7tW3DyoEVXLiV+IXg=";

    // Use this for initialization
    void Start () {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        username.text = userAccountInfo.userName;

    }
	
}
