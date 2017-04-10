using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountMenuGamesWon : MonoBehaviour {

    public Text gamesWon;
    public int user = 4;

    // Use this for initialization
    void Start()
    {
        NetworkData.AccountInfo userAccountInfo = NetworkController.FetchAccountInfo(user);
        gamesWon.text = userAccountInfo.gamesWon.ToString();
    }
}
