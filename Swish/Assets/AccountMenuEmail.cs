using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuEmail : MonoBehaviour
{

    public Text email;
    public int user = 601;

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        email.text = userAccountInfo.userEmail;
    }
}