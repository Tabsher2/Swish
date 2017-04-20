
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
//old


namespace NetworkData
{

    [Serializable]
    public class UsernameRequest
    {
        public string username;
    }

    [Serializable]
    public class UsernameResponse
    {
        public int userID;
        public string error;

    }

    public class GetUserID : MonoBehaviour
    {

        private static UsernameResponse responseMessage = new UsernameResponse();
        private static UsernameRequest requestObject = new UsernameRequest();

        public static int RetrieveUserID(string username)
        {
            string url = "http://swishgame.com/AppCode/GetUserIDFromUsername.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.username = username;
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
            responseMessage = JsonUtility.FromJson<UsernameResponse>(responseFromServer);
            int userID = responseMessage.userID;
            Debug.Log("friendID erreo" + responseMessage.error);
            return userID;
            // Display the content.  
        }
    }
}



