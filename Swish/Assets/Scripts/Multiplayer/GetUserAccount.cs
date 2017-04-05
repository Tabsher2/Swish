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
    public class UserId
    {
        public string user;
    }

    [Serializable]
    public class AccountInfo
    {
        public string userName;
        public string userEmail;
        public string gamesWon;
        public string swishesMade;
        public string winStreak;
        public string error;
    }

    public class FetchAccountInfo : MonoBehaviour
    {

        private static AccountInfo responseMessage = new AccountInfo();
        private static UserId requestObject = new UserId();

        public static AccountInfo RetrieveUserAccountInfo(string user)
        {
            string url = "http://swishgame.com/AppCode/GetUserAccount.aspx";
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
            responseMessage = JsonUtility.FromJson<AccountInfo>(responseFromServer);
            responseMessage.userName = responseMessage.userName.Trim();
            responseMessage.userEmail = responseMessage.userEmail.Trim();
            responseMessage.gamesWon = responseMessage.gamesWon.Trim();
            responseMessage.swishesMade = responseMessage.swishesMade.Trim();
            responseMessage.winStreak = responseMessage.winStreak.Trim();
            return responseMessage;
            // Display the content.  
        }
    }
}

