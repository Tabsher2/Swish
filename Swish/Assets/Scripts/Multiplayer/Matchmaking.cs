using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace NetworkData
{
    public class Matchmaking : MonoBehaviour
    {
        [Serializable]
        public class UserData
        {
            public int userID;
        }

        [Serializable]
        public class Opponent
        {
            public string username;
            public int opponentID;
            public string error;
        }

        public static int FindOpponent(int user)
        {
            Opponent responseMessage = new Opponent();
            UserData requestObject = new UserData();

            string url = "http://swishgame.com/AppCode/Matchmaking.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.userID = user;

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
            responseMessage = JsonUtility.FromJson<Opponent>(responseFromServer);
            return responseMessage.opponentID;
            // Display the content.  
        }
    }

}
