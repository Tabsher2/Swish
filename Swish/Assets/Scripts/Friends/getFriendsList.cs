using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace NetworkData
{
    [Serializable]
    public class userIDfriends
    {
        public int user;
    }

    [Serializable]
    public class friendsIDs
    {
        public int friendID;

        public friendsIDs(int friendID)
        {
            this.friendID = friendID;
        }
    }

    [Serializable]
    public class friendList
    {
        public List<friendsIDs> friendIDList;
        public string error;
    }

    public class getFriendsList : MonoBehaviour
    {

        private static friendList responseMessage = new friendList();
        private static userIDfriends requestObject = new userIDfriends();

        public static List<friendsIDs> getFriends(int user)
        {
            string url = "http://swishgame.com/AppCode/GetFriendsList.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.user = user;
            string jsonString = JsonUtility.ToJson(requestObject);
            byte[] data = Encoding.UTF8.GetBytes(jsonString);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);

            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            responseMessage = JsonUtility.FromJson<friendList>(responseFromServer);
            //Debug.Log(responseFromServer);
            List<friendsIDs> userGames = responseMessage.friendIDList;
            return userGames;
            // Display the content.  
        }
    }
}




