using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuEmail : MonoBehaviour
{

    public Text email;
    public string user = "/liCWn1QSAx8RCfPOI+c42aXrD7tW3DyoEVXLiV+IXg=";

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        email.text = userAccountInfo.userEmail;
    }
}