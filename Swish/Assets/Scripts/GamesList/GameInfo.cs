using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameInfoForLoad
{
    public class GameInfo : MonoBehaviour
    {

        public int gameID;

        private void Awake()
        {
            gameID = PlayerPrefs.GetInt("userID");
            DontDestroyOnLoad(this);
        }


    }
}
