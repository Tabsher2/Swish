using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuSwishesMade : MonoBehaviour {

    public Text swishesMade;
    public string user = "/liCWn1QSAx8RCfPOI+c42aXrD7tW3DyoEVXLiV+IXg=";

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        swishesMade.text = userAccountInfo.swishesMade;
    }
}
