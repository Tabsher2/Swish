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
    public class LeaderboardTitle
    {
        public string LeaderboardTitleString;
    }

    [Serializable]
    public class statLeaderName
    {
        public string statLeader;

        public statLeaderName(string statLeader)
        {
            this.statLeader = statLeader;
        }
    }

    [Serializable]
    public class statLeaderWins
    {
        public int statLeader;

        public statLeaderWins(int statLeader)
        {
            this.statLeader = statLeader;
        }
    }

    [Serializable]
    public class statLeaderList
    {
        public List<statLeaderName> leaderNames;
        public List<statLeaderWins> leaderWins;
        public string error;
    }

    public class getLeaderboards : MonoBehaviour
    {

        private static LeaderboardTitle requestObject = new LeaderboardTitle();
        private static statLeaderList responseMessage = new statLeaderList();

        public static List<statLeaderName> RetrieveLeaders(string leaderboardTitle)//only gets win leader usernames -- use "totalWins"
        {
            string url = "http://swishgame.com/AppCode/GetLeaderboard.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.LeaderboardTitleString = leaderboardTitle;
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
            responseMessage = JsonUtility.FromJson<statLeaderList>(responseFromServer);

            List<statLeaderName> leaderNames = responseMessage.leaderNames;
            return leaderNames;
            // Display the content.  
        }

        public static List<statLeaderWins> RetrieveLeadersWins(string leaderboardTitle)//only gets win leaders win stats "totalWins"
        {
            string url = "http://swishgame.com/AppCode/GetLeaderboardWins.aspx";
            WebRequest request = WebRequest.Create(url);
            requestObject.LeaderboardTitleString = leaderboardTitle;
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
            responseMessage = JsonUtility.FromJson<statLeaderList>(responseFromServer);

            List<statLeaderWins> leaderWins = responseMessage.leaderWins;

            return leaderWins;
            // Display the content.  
        }
    }
}



