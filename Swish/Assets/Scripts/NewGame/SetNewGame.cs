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
    public class AddGameRequest
    {
        public int user;
        public int opponent;
        public int gameLength;
    }

    [Serializable]
    public class AddGameResponse
    {
        public int gameID;
        public string message;
        public string error;

    }

    public class SetNewGame : MonoBehaviour
    {

        private static AddGameResponse responseMessage = new AddGameResponse();
        private static AddGameRequest requestObject = new AddGameRequest();

        public static int RetrieveGames(int user, int opponent, int gameLength)
        {
            string url = "http://swishgame.com/AppCode/AddGames.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.user = user;
            requestObject.opponent = opponent;
            requestObject.gameLength = gameLength;
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
            responseMessage = JsonUtility.FromJson<AddGameResponse>(responseFromServer);

            int gameID = responseMessage.gameID;
            Debug.Log("AddGame message: " + responseMessage.message);
            Debug.Log("AddGame Error: " + responseMessage.error);
            return gameID;
            // Display the content.  
        }
    }
}




