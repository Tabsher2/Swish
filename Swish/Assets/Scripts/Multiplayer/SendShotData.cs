﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using GameInfoForLoad;

namespace NetworkData
{
    public class SendShotData : MonoBehaviour
    {
        public static bool allowMenu = false;


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
            public int gameID;
        }

        [Serializable]
        public class MadeShotRequest
        {
            public int gameID;
            public int p1score;
            public int p2score;
            public float locationX;
            public float locationZ;
            public int currentTurnOwner;
            public int turnNo;
            public int shotMade;
            public float ballX;
            public float ballY;
            public float ballZ;
            public int shotScore;
        }

        [Serializable]
        public class AddLetterRequest
        {
            public int p1letters;
            public int p2letters;
            public int failedShot;
            public int gameID;
        }

        [Serializable]
        public class CopyShotRequest
        {
            public int gameID;
            public int shotStatus;
        }

        [Serializable]
        public class ObstacleRequest
        {
            public int gameID;
            public List<ScoreAccumulator.Obstacle> hitObstacles;
            public List<ScoreAccumulator.Obstacle> placedObstacles;
        }

        [Serializable]
        public class ObstacleResponse
        {
            public List<ScoreAccumulator.Obstacle> hitObstacles;
            public List<ScoreAccumulator.Obstacle> placedObstacles;
            public string error;
        }

        [Serializable]
        public class BaseRequest
        {
            public int gameID;
        }




        public static void SendMissedShot(int turnOwnerId, int turnNo, int gameID)
        {
            ServerResponse responseMessage = new ServerResponse();
            MissShotRequest requestObject = new MissShotRequest();

            string url = "http://swishgame.com/AppCode/MissedShot.aspx";
            WebRequest request = WebRequest.Create(url);
            //A 0 since the user missed
            requestObject.shotMade = 0;
            requestObject.currentTurnOwner = turnOwnerId;
            requestObject.turnNo = turnNo;
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
            responseMessage = JsonUtility.FromJson<ServerResponse>(responseFromServer);
            // Display the content.  
        }

        public static void SendMadeShot(int player, int turnOwnerId, int userScore, float locationX, float locationZ, int turnNo, float ballX, float ballY, float ballZ, int gameID, int shotScore)
        {
            ServerResponse responseMessage = new ServerResponse();
            MadeShotRequest requestObject = new MadeShotRequest();

            string url = "http://swishgame.com/AppCode/MadeShot.aspx";
            WebRequest request = WebRequest.Create(url);

            requestObject.gameID = gameID;
            requestObject.shotScore = shotScore;
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
            // Display the content.  
            allowMenu = true;
        }

        public static void AddLetter(int player, int userLetters, int failedShot, int gameID)
        {
            ServerResponse responseMessage = new ServerResponse();
            AddLetterRequest requestObject = new AddLetterRequest();

            string url = "http://swishgame.com/AppCode/AddLetter.aspx";
            WebRequest request = WebRequest.Create(url);

            requestObject.gameID = gameID;
            requestObject.failedShot = failedShot;
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
            // Display the content.  
        }

        public static void CopyShotResult(int result, int gameID)
        {
            ServerResponse responseMessage = new ServerResponse();
            CopyShotRequest requestObject = new CopyShotRequest();

            string url = "http://swishgame.com/AppCode/UpdateCopyResult.aspx";
            WebRequest request = WebRequest.Create(url);

            requestObject.gameID = gameID;
            requestObject.shotStatus = result;

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
            // Display the content.  
        }

        public static void SendObstacles(List<ScoreAccumulator.Obstacle> hitObstacles, List<ScoreAccumulator.Obstacle> placedObstacles, int gameID)
        {
            ServerResponse responseMessage = new ServerResponse();
            ObstacleRequest requestObject = new ObstacleRequest();

            string url = "http://swishgame.com/AppCode/PostObstacles.aspx";
            WebRequest request = WebRequest.Create(url);
            //A 0 since the user missed
            requestObject.gameID = gameID;
            requestObject.placedObstacles = new List<ScoreAccumulator.Obstacle>();
            requestObject.placedObstacles = placedObstacles;
            if (requestObject.placedObstacles.Count == 0)
                return;
            requestObject.hitObstacles = new List<ScoreAccumulator.Obstacle>();
            requestObject.hitObstacles = hitObstacles;
            if (requestObject.hitObstacles.Count == 0)
                return;

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
            // Display the content.  
        }

        public static void LoadObstacles(int gameID)
        {
            ObstacleResponse responseMessage = new ObstacleResponse();
            BaseRequest requestObject = new BaseRequest();

            string url = "http://swishgame.com/AppCode/LoadObstacles.aspx";
            WebRequest request = WebRequest.Create(url);
            //A 0 since the user missed
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
            responseMessage = JsonUtility.FromJson<ObstacleResponse>(responseFromServer);
            for (int i = 0; i < responseMessage.hitObstacles.Count; i++)
            {
                GameController.usedObstacles.Add(new ScoreAccumulator.Obstacle(responseMessage.hitObstacles[i].obstacleID, responseMessage.hitObstacles[i].locationX, responseMessage.hitObstacles[i].locationY, responseMessage.hitObstacles[i].locationZ));
            }
            for (int i = 0; i < responseMessage.placedObstacles.Count; i++)
            {
                GameController.mapObstacles.Add(new ScoreAccumulator.Obstacle(responseMessage.placedObstacles[i].obstacleID, responseMessage.placedObstacles[i].locationX, responseMessage.placedObstacles[i].locationY, responseMessage.placedObstacles[i].locationZ));
            }
            // Display the content.  
        }

        public static void ClearObstacles(int gameID)
        {
            ServerResponse responseMessage = new ServerResponse();
            BaseRequest requestObject = new BaseRequest();

            string url = "http://swishgame.com/AppCode/ClearObstacles.aspx";
            WebRequest request = WebRequest.Create(url);
            //A 0 since the user missed
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
            responseMessage = JsonUtility.FromJson<ServerResponse>(responseFromServer);
            // Display the content.  
        }
    }
}


