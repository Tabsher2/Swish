using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUSerCurrency : MonoBehaviour {

    public Text currency;

	// Use this for initialization
	void Start () {
        getUserCurrency();
	}
	
    void getUserCurrency()
    {
        NetworkData.AccountInfo userAccount = NetworkController.FetchAccountInfo(PlayerPrefs.GetInt("userID"));

        currency.text = userAccount.userCurrency;
    }

}
