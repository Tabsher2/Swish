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
    public class AddRequest
    {
        public string username;
        public string email;
        public string pw;
        public string dob;
    }

    [Serializable]
    public class AddResponse
    {
        public string message;
        public string error;
    }

    public class GetAppSignUp : MonoBehaviour
    {

        private static AddResponse responseMessage = new AddResponse();
        private static AddRequest requestObject = new AddRequest();

        public static string AddUser(string username, string email, string pw, string dob)
        {
            string url = "http://swishgame.com/JSONServices/AppAddUser.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.username = username;
            requestObject.email = email;
            requestObject.pw = pw;
            requestObject.dob = dob;
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
            responseMessage = JsonUtility.FromJson<AddResponse>(responseFromServer);

            string error = responseMessage.error;
            Debug.Log("Error Add user: " + responseMessage.error);
            return responseMessage.message;
            // Display the content.  
        }
    }
}



