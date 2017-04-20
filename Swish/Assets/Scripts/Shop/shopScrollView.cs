using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using friendsListInfo;

public class shopScrollView : MonoBehaviour
{
    private int user = 4;
    public GameObject gamePrefab;
    public GameObject gamePrefabHeader;
    public GameObject scrollViewContentShop;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject temp;
            if (i == 0)
                temp = Instantiate(gamePrefabHeader) as GameObject;
            else
                temp = Instantiate(gamePrefab) as GameObject;
            
            temp.transform.SetParent(scrollViewContentShop.transform, false);
        }
        //List<NetworkData.friendsIDs> userFriendsInfo = NetworkController.getFriends(user);
        //List<string> userFriendNames = new List<string>();

        // for (int i = 0; i < userFriendsInfo.Count; i++)
        //  {
        //      userFriendNames.Add(NetworkController.FetchAccountInfo(userFriendsInfo[i].friendID).userName);
        //  }

        //  getFriendsList(userFriendNames, userFriendsInfo);
    }

    /*   void getFriendsList(List<string> userFriendNames, List<NetworkData.friendsIDs> userFriendsInfo)
       {

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
       }*/
}
