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
    public struct AddFriendRequest
    {
        public int userID;
        public int friendID;
    }

    [Serializable]
    public struct AddFriendResponse
    {
        public string message;
        public string error;

    }

    public class AddUserFriends : MonoBehaviour
    {

        private static AddFriendResponse responseMessage = new AddFriendResponse();
        private static AddFriendRequest requestObject = new AddFriendRequest();

        public static void AddFriends(int userID, int friendID)
        {
            Debug.Log("here");
            string url = "http://swishgame.com/AppCode/AppAddFriend.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.userID = userID;
            requestObject.friendID = friendID;
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
            responseMessage = JsonUtility.FromJson<AddFriendResponse>(responseFromServer);

            Debug.Log("AddGame message: " + responseMessage.message);
            Debug.Log("AddGame Error: " + responseMessage.error);
            // Display the content.  
        }
    }
}






