using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;


namespace NetworkData
{
    public class UpdateLeaderboards : MonoBehaviour
    {
        [Serializable]
        public class SwishData
        {
            public int userID;
        }

        [Serializable]
        public class BaseResponse
        {
            public string message;
            public string error;
        }

        [Serializable]
        public class ShotStreakData
        {
            public int userID;
            public int increment;
        }

        [Serializable]
        public class EndGameContent
        {
            public int userID;
            public int score;
            public int result;
            public int userLetters;
        }

        [Serializable]
        public class TimeInfo
        {
            public int userID;
            public float timePlayed;
        }

        public static void AddSwish(int user)
        {
            BaseResponse responseMessage = new BaseResponse();
            SwishData requestObject = new SwishData();

            string url = "http://swishgame.com/AppCode/AddSwish.aspx";
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
            responseMessage = JsonUtility.FromJson<BaseResponse>(responseFromServer);
            // Display the content.  
        }

        public static void UpdateShotStreak(int user, int increment)
        {
            BaseResponse responseMessage = new BaseResponse();
            ShotStreakData requestObject = new ShotStreakData();

            string url = "http://swishgame.com/AppCode/UpdateShotStreak.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.userID = user;
            requestObject.increment = increment;

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
            responseMessage = JsonUtility.FromJson<BaseResponse>(responseFromServer);
            // Display the content.  
        }

        public static void UpdateGameEnd(int user, int userScore, int result, int userLetters)
        {
            BaseResponse responseMessage = new BaseResponse();
            EndGameContent requestObject = new EndGameContent();

            string url = "http://swishgame.com/AppCode/UpdateGameEnd.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.userID = user;
            requestObject.score = userScore;
            requestObject.result = result;
            requestObject.userLetters = userLetters;

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
            responseMessage = JsonUtility.FromJson<BaseResponse>(responseFromServer);
            // Display the content.
        }

        public static void UpdateTime(int user, float gameTime)
        {
            BaseResponse responseMessage = new BaseResponse();
            TimeInfo requestObject = new TimeInfo();

            string url = "http://swishgame.com/AppCode/UpdateTimePlayed.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.userID = user;
            requestObject.timePlayed = gameTime;

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
            responseMessage = JsonUtility.FromJson<BaseResponse>(responseFromServer);
            // Display the content.
        }

    }   
	
}
