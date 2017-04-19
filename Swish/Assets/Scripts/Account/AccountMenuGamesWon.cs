using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuGamesWon : MonoBehaviour {

    public Text gamesWon;
    private int user;

    // Use this for initialization
    void Start()
    {
        user = PlayerPrefs.GetInt("userID");
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        gamesWon.text = userAccountInfo.gamesWon.ToString();
    }
}
