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
    public class UserData
    {
        public string userName;
        public string opponentName;
        public string error;
    }

    [Serializable]
    public class PlayerInfo
    {
        public string user;
        public string opponent;
    }

    public class FetchUserData : MonoBehaviour
    {

        private static UserData responseMessage = new UserData();
        private static PlayerInfo requestObject = new PlayerInfo();

        public static UserData RetrieveUserInfo(string user, string opponent)
        {
            string url = "http://swishgame.com/AppCode/GetUserData.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.user = user;
            requestObject.opponent = opponent;
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
            responseMessage = JsonUtility.FromJson<UserData>(responseFromServer);
            responseMessage.userName = responseMessage.userName.Trim();
            responseMessage.opponentName = responseMessage.opponentName.Trim();
            return responseMessage;
            // Display the content.  
        }
    }
}

