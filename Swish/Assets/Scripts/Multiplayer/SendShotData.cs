using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace NetworkData
{
    public class SendShotData : MonoBehaviour
    {

        [Serializable]
        public class ServerResponse
        {
            public string message;
            public string error;
        }

        [Serializable]
        public class MissShotRequest
        {
            public int shotMade;
            public int currentTurnOwner;
            public int turnNo;
        }

        [Serializable]
        public class MadeShotRequest
        {
            public float p1score;
            public float p2score;
            public float locationX;
            public float locationZ;
            public int currentTurnOwner;
            public int turnNo;
            public int shotMade;
            public float ballX;
            public float ballY;
            public float ballZ;
        }

        [Serializable]
        public class AddLetterRequest
        {
            public int p1letters;
            public int p2letters;
        }


        public static void SendMissedShot(int turnOwnerId, int turnNo)
        {
            ServerResponse responseMessage = new ServerResponse();
            MissShotRequest requestObject = new MissShotRequest();

            string url = "http://swishgame.com/AppCode/MissedShot.aspx";
            WebRequest request = WebRequest.Create(url);
            //A 0 since the user missed
            requestObject.shotMade = 0;
            requestObject.currentTurnOwner = turnOwnerId;
            requestObject.turnNo = turnNo;

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
            responseMessage = JsonUtility.FromJson<ServerResponse>(responseFromServer);
            Debug.Log(responseMessage.message);
            // Display the content.  
        }

        public static void SendMadeShot(int player, int turnOwnerId, float userScore, float locationX, float locationZ, int turnNo, float ballX, float ballY, float ballZ)
        {
            ServerResponse responseMessage = new ServerResponse();
            MadeShotRequest requestObject = new MadeShotRequest();

            string url = "http://swishgame.com/AppCode/MadeShot.aspx";
            WebRequest request = WebRequest.Create(url);

            //A 1 since the user made it
            requestObject.shotMade = 1;
            if (player == 1)
            {
                requestObject.p1score = userScore;
                requestObject.p2score = -1;
            }
            else
            {
                requestObject.p2score = userScore;
                requestObject.p1score = -1;
            }
            requestObject.currentTurnOwner = turnOwnerId;
            requestObject.locationX = locationX;
            requestObject.locationZ = locationZ;
            requestObject.turnNo = turnNo;
            requestObject.ballX = ballX;
            requestObject.ballY = ballY;
            requestObject.ballZ = ballZ;

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
            responseMessage = JsonUtility.FromJson<ServerResponse>(responseFromServer);
            Debug.Log(responseMessage.message);
            // Display the content.  
        }

        public static void AddLetter(int player, int userLetters)
        {
            ServerResponse responseMessage = new ServerResponse();
            AddLetterRequest requestObject = new AddLetterRequest();

            string url = "http://swishgame.com/AppCode/AddLetter.aspx";
            WebRequest request = WebRequest.Create(url);


            if (player == 1)
            {
                requestObject.p1letters = userLetters;
                requestObject.p2letters = -1;
            }
            else
            {
                requestObject.p2letters = userLetters;
                requestObject.p1letters = -1;
            }

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
            responseMessage = JsonUtility.FromJson<ServerResponse>(responseFromServer);
            Debug.Log(responseMessage.message);
            // Display the content.  
        }
    }
}


