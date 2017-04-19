using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using setUpGameInformantion;
namespace lengthPanel
    {
    public class gameLengthPanelActivate : MonoBehaviour {
        public GameObject thisObject;
        public GameObject setUpInfo;

        // Use this for initialization
        void Start()
        {

            setUpInfo = GameObject.Find("SetupGameInfo");
        }

        // Update is called once per frame

    }
}