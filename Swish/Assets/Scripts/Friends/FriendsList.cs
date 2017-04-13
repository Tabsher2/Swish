﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using friendsListInfo;

public class FriendsList : MonoBehaviour {

    private int user = 4;
    public GameObject gamePrefab;
    public GameObject scrollViewContentFriends;

    // Use this for initialization
    void Start()
    {
        List<NetworkData.friendsIDs> userFriendsInfo = NetworkController.getFriends(user);
        List<string> userFriendNames = new List<string>();

        for (int i = 0; i < userFriendsInfo.Count; i++)
        {
            userFriendNames.Add(NetworkController.FetchAccountInfo(userFriendsInfo[i].friendID).userName);
        }

        getFriendsList(userFriendNames, userFriendsInfo);
    }

    void getFriendsList(List<string> userFriendNames, List<NetworkData.friendsIDs> userFriendsInfo)
    {

        //initialize games where its the users turn
        for (int i = 0; i < userFriendNames.Count; i++)
        {
                GameObject temp = Instantiate(gamePrefab) as GameObject;
                temp.GetComponent<friendListPlayerID>().friendIDname = userFriendsInfo[i].friendID;
                temp.GetComponent<friendListPlayerID>().friendName.text = userFriendNames[i];
                temp.transform.SetParent(scrollViewContentFriends.transform, false);
        }
    }
}
