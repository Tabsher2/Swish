using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace setUpGameInformantion
{
    public class setUpGameInfo : MonoBehaviour
    {
        public GameObject panel1;
        public int userID;
        public int opponentID;
        public int turnLength;
        void Start()
        {
            userID = PlayerPrefs.GetInt("userID");
            
        }

        public void activatePanel()
        {
            panel1.SetActive(true);
        }


    }
}
