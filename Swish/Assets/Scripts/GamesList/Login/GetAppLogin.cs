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
    public class LoginRequest
    {
        public string email;
        public string pw;
    }

    [Serializable]
    public class LoginResponse
    {
        public string id;
        public string error;
        public string message;
    }

    public class GetAppLogin : MonoBehaviour
    {

        private static LoginResponse responseMessage = new LoginResponse();
        private static LoginRequest requestObject = new LoginRequest();

        public static int Login(string email, string pw)
        {
            string url = "http://swishgame.com/JSONServices/AppLogin.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.email = email;
            requestObject.pw = pw;
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
            responseMessage = JsonUtility.FromJson<LoginResponse>(responseFromServer);

            string id;
            string error = responseMessage.error;
            if(error.Equals(""))
            {
                id = responseMessage.id;
            }
            else
            {
                Debug.Log(responseMessage.message);
                id = "-111";
            }
            Debug.Log(id);
            if (id == "FAILURE!")
                return -1;
            else
                return Convert.ToInt32(id);
            // Display the content.  
        }
    }
}



