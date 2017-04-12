using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace NetworkData
{
    public class GetShotData : MonoBehaviour
    {
        private static ShotData responseMessage = new ShotData();
        private static GameInfo requestObject = new GameInfo();

        public static ShotData RetrieveGameInfo(int gameID)
        {
            string url = "http://swishgame.com/AppCode/GetShotData.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.gameID = gameID;
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
            responseMessage = JsonUtility.FromJson<ShotData>(responseFromServer);
            return responseMessage;
            // Display the content.  
        }
    }

    [Serializable]
    public class ShotData
    {
        public int player1;
        public int player2;
        public int p1score;
        public int p2score;
        public int p1letters;
        public int p2letters;
        public float locationX;
        public float locationZ;
        public int currentTurnOwner;
        public int turnNo;
        public int shotMade;
        public float ballX;
        public float ballY;
        public float ballZ;
        public int shotStatus;
        public int gameLength;
        public string error;
    }

    [Serializable]
    public class GameInfo
    {
        public int gameID;
    }
}


