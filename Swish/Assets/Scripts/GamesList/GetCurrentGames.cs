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
    public class GamePlayerID
    {
        public int user;
    }

    [Serializable]
    public class CurrentGameInfo
    {
        public int opponentID;
        public int gameID;
        public int turn;

        public CurrentGameInfo(int opponentID, int gameID)
        {
            this.opponentID = opponentID;
            this.gameID = gameID;
            this.turn = turn;
        }
    }

    [Serializable]
    public class GameIDs
    {
        public List<CurrentGameInfo> Games;
        public string error;
    }

    public class GetCurrentGamesInfo : MonoBehaviour
    {

        private static GameIDs responseMessage = new GameIDs();
        private static GamePlayerID requestObject = new GamePlayerID();

        public static List<CurrentGameInfo> RetrieveGames(int user)
        {
            string url = "http://swishgame.com/AppCode/GetCurrentGames.aspx";
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
            responseMessage = JsonUtility.FromJson<GameIDs>(responseFromServer);
            Debug.Log(responseFromServer);
            List<CurrentGameInfo> userGames = responseMessage.Games;
            return userGames;
            // Display the content.  
        }
    }
}


