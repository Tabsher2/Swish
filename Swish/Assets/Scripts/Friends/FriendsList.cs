using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using friendsListInfo;

namespace friends
{


    public class FriendsList : MonoBehaviour
    {

        private int user;
        public GameObject gamePrefab;
        public GameObject gamePrefabHeader;
        public GameObject scrollViewContentFriends;
        private List<NetworkData.friendsIDs> userFriendsInfo = new List<NetworkData.friendsIDs>();
        private List<string> userFriendNames = new List<string>();

        // Use this for initialization
        void Start()
        {
            user = PlayerPrefs.GetInt("userID");


            getFriendsList();
        }

        public void getFriendsList()
        {
            var children = new List<GameObject>();
            foreach (Transform child in scrollViewContentFriends.transform)
            {
                children.Add(child.gameObject);
            }
            children.ForEach(child => Destroy(child));

            if (userFriendNames.Count != 0)
            {
                userFriendNames.Clear();
                userFriendsInfo.Clear();
            }

            userFriendsInfo = NetworkController.getFriends(user);
            userFriendNames = new List<string>();

            for (int i = 0; i < userFriendsInfo.Count; i++)
            {
                userFriendNames.Add(NetworkController.FetchAccountInfo(userFriendsInfo[i].friendID).userName);
            }

            //initialize games where its the users turn
            for (int i = 0; i < userFriendNames.Count; i++)
            {
                GameObject temp;
                if (i == 0)
                    temp = Instantiate(gamePrefabHeader) as GameObject;
                else
                    temp = Instantiate(gamePrefab) as GameObject;
                temp.GetComponent<friendListPlayerID>().friendIDname = userFriendsInfo[i].friendID;
                temp.GetComponent<friendListPlayerID>().friendName.text = userFriendNames[i];
                temp.GetComponent<friendListPlayerID>().friendName.color = new Color(1, 1, 1, 1);
                temp.transform.SetParent(scrollViewContentFriends.transform, false);
            }
        }
    }
}
