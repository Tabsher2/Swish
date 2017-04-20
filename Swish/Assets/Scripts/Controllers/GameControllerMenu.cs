using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using gameListInfo;
using System;
using leaderboard;
using friends;
using account;

namespace GCmenu
{
    public class GameControllerMenu : MonoBehaviour
    {

        public GameObject homeMenu;
        public GameObject menu1Menu;
        public GameObject menu2Menu;
        public GameObject menu4Menu;
        public GameObject menu5Menu;
        public GameObject activeMenu;
        public GameObject gameLoadingPanel;

        public GameObject gamePrefab;
        public GameObject yourTurnPrefab;
        public GameObject opponentsTurnPrefab;
        public GameObject scrollViewContent;

        public GameObject leaderboardMenu;
        public GameObject friendsMenu;
        public GameObject accountMenu;

        public Button practice;

        public Toggle button1;
        public Toggle button2;
        public Toggle buttonHome;
        public Toggle button4;
        public Toggle button5;

        public static bool refresh = false;

        public int user;

        // Use this for initialization
        void Awake()
        {
            homeMenu.SetActive(true);
            activeMenu = homeMenu;
            menu1Menu.SetActive(false);
            menu2Menu.SetActive(false);
            menu4Menu.SetActive(false);
            menu5Menu.SetActive(false);

            practice.onClick.AddListener(() => PracticeClicked());
            user = PlayerPrefs.GetInt("userID");
        }

        void Start()
        {
            buttonHome.Select();
            Debug.Log("Current games for user: " + user);
            getCurrentGames(user);
        }

        // Update is called once per frame
        void Update()
        {
            if (refresh)
            {
                getCurrentGames(user);
                leaderboardMenu.GetComponent<leaderBoardDisplay>().displayLeaderBoard();
                friendsMenu.GetComponent<FriendsList>().getFriendsList();
                accountMenu.GetComponent<userAccountInfo>().setData();
                refresh = false;
            }

            if (button1.isOn)
            {
                activeMenu.SetActive(false);
                menu1Menu.SetActive(true);
                activeMenu = menu1Menu;
            }
            if (button2.isOn)
            {
                activeMenu.SetActive(false);
                menu2Menu.SetActive(true);
                activeMenu = menu2Menu;
            }
            if (buttonHome.isOn)
            {
                activeMenu.SetActive(false);
                homeMenu.SetActive(true);
                activeMenu = homeMenu;
            }
            if (button4.isOn)
            {
                activeMenu.SetActive(false);
                menu4Menu.SetActive(true);
                activeMenu = menu4Menu;
            }
            if (button5.isOn)
            {
                activeMenu.SetActive(false);
                menu5Menu.SetActive(true);
                activeMenu = menu5Menu;
            }



        }

        public void getCurrentGames(int user)
        {
            //get users games
            List<NetworkData.CurrentGameInfo> userGames = NetworkController.RetrieveGames(user);
            List<NetworkData.CurrentGameInfo> opponentsTurnGames = new List<NetworkData.CurrentGameInfo>();

            PlayerPrefs.SetInt("gameCount", userGames.Count);

            var children = new List<GameObject>();
            foreach (Transform child in scrollViewContent.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));
            //initialize games where its the users turn
            int gamesMade = 0;
            for (int i = 0; i < userGames.Count; i++)
            {
                if (userGames[i].turn != user)
                {
                    opponentsTurnGames.Add(userGames[i]);
                }
                else
                {
                    GameObject temp;
                    if (gamesMade == 0)
                        temp = Instantiate(yourTurnPrefab) as GameObject;
                    else
                        temp = Instantiate(gamePrefab) as GameObject;
                    temp.GetComponent<currentGameContentInfo>().gameID = userGames[i].gameID;
                    temp.GetComponent<currentGameContentInfo>().opponentID = userGames[i].opponentID;
                    temp.GetComponent<currentGameContentInfo>().turn = userGames[i].turn;
                    temp.GetComponent<currentGameContentInfo>().opponentName.text = NetworkController.FetchAccountInfo(temp.GetComponent<currentGameContentInfo>().opponentID).userName;
                    temp.transform.SetParent(scrollViewContent.transform, false);
                    gamesMade++;
                }
            }


            //display games where it is the opponents turn
            gamesMade = 0;
            for (int i = 0; i < opponentsTurnGames.Count; i++)
            {
                GameObject temp2;
                if (gamesMade == 0)
                    temp2 = Instantiate(opponentsTurnPrefab) as GameObject;
                else
                    temp2 = Instantiate(gamePrefab) as GameObject;
                temp2.GetComponent<currentGameContentInfo>().gameID = opponentsTurnGames[i].gameID;
                temp2.GetComponent<currentGameContentInfo>().opponentID = opponentsTurnGames[i].opponentID;
                temp2.GetComponent<currentGameContentInfo>().turn = opponentsTurnGames[i].turn;
                temp2.GetComponent<currentGameContentInfo>().opponentName.text = NetworkController.FetchAccountInfo(temp2.GetComponent<currentGameContentInfo>().opponentID).userName;
                temp2.transform.SetParent(scrollViewContent.transform, false);
                gamesMade++;
            }
        }

        void PracticeClicked()
        {
            gameLoadingPanel.SetActive(true);
            practice.onClick.RemoveListener(() => PracticeClicked());

            SceneManager.LoadScene("Practice", LoadSceneMode.Single);


        }

    }
}
