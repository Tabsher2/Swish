using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkData;
using System;

public class NetworkController : MonoBehaviour
{

    public static void SendMissedShot(int turnOwnerId, int turnNo, int gameID)
    {
        NetworkData.SendShotData.SendMissedShot(turnOwnerId, turnNo, gameID);
    }

    public static void SendMadeShot(int player, int turnOwnerId, int userScore, float locationX, float locationZ, int turnNo, float ballX, float ballY, float ballZ, int gameID)
    {
        NetworkData.SendShotData.SendMadeShot(player, turnOwnerId, userScore, locationX, locationZ, turnNo, ballX, ballY, ballZ, gameID);
    }

    public static void AddLetter(int player, int userLetters, int failedShot, int gameID)
    {
        NetworkData.SendShotData.AddLetter(player, userLetters, failedShot, gameID);
    }

    public static void SendObstacles(List<ScoreAccumulator.Obstacle> hitObstacles, List<ScoreAccumulator.Obstacle> placedObstacles, int gameID)
    {
        NetworkData.SendShotData.SendObstacles(hitObstacles, placedObstacles, gameID);
    }

    public static void LoadObstacles(int gameID)
    {
        NetworkData.SendShotData.LoadObstacles(gameID);
    }

    public static void ClearObstacles(int gameID)
    {
        NetworkData.SendShotData.ClearObstacles(gameID);
    }

    public static void AddSwish(int user)
    {
        NetworkData.UpdateLeaderboards.AddSwish(user);
    }

    public static void UpdateShotStreak(int user, int increment)
    {
        NetworkData.UpdateLeaderboards.UpdateShotStreak(user, increment);
    }

    public static void UpdateGameEnd(int user, int userScore, int result, int userLetters, int gameID)
    {
        NetworkData.UpdateLeaderboards.UpdateGameEnd(user, userScore, result, userLetters, gameID);
    }

    public static void UpdateTime(int user, float gameTime)
    {
        NetworkData.UpdateLeaderboards.UpdateTime(user, gameTime);
    }

    public static void UpdateCopyResult(int result, int gameID)
    {
        NetworkData.SendShotData.CopyShotResult(result, gameID);
    }

    public static NetworkData.ShotData LoadLastShot(int gameID)
    {
        NetworkData.ShotData shotData = NetworkData.GetShotData.RetrieveGameInfo(gameID);
        return shotData;
    }

    public static NetworkData.UserData FetchUserData(int user, int opponent)
    {
        NetworkData.UserData userData = NetworkData.FetchUserData.RetrieveUserInfo(user, opponent);
        return userData;
    }

    public static NetworkData.AccountInfo FetchAccountInfo(int user)
    {
        NetworkData.AccountInfo userAccountInfo = NetworkData.FetchAccountInfo.RetrieveUserAccountInfo(user);
        return userAccountInfo;
    }

    public static List<NetworkData.CurrentGameInfo> RetrieveGames(int user)
    {
        List<NetworkData.CurrentGameInfo> gameInfo = GetCurrentGamesInfo.RetrieveGames(user);
        return gameInfo;
    }

    public static int FindOpponent(int user)
    {
        int opponentID = NetworkData.Matchmaking.FindOpponent(user);
        return opponentID;
    }

    public static List<NetworkData.friendsIDs> getFriends(int user)
    {
        List<NetworkData.friendsIDs> friendsNames = getFriendsList.getFriends(user);
        return friendsNames;
    }
    public static List<NetworkData.statLeaderName> getLeaderboard(string LeaderboardTitle)
    {
        List<NetworkData.statLeaderName> statLeaderList = getLeaderboards.RetrieveLeaders(LeaderboardTitle);
        return statLeaderList;
    }

    public static List<NetworkData.statLeaderWins> getLeaderboardWins(string LeaderboardTitle)
    {
        List<NetworkData.statLeaderWins> statLeaderWinsList = getLeaderboards.RetrieveLeadersWins(LeaderboardTitle);
        return statLeaderWinsList;
    }

    public static int Login(string email, string pw)
    {
        int userID = NetworkData.GetAppLogin.Login(email, pw);
        return userID;
    }

    public static string AddUser(string username, string email, string pw, string dob)
    {
        string added = NetworkData.GetAppSignUp.AddUser(username, email, pw, dob);
        return added;
    }

    public static int AddGame(int user, int opponent, int gameLength)
    {
        
        int newGameID = NetworkData.SetNewGame.RetrieveGames(user, opponent, gameLength);
        return newGameID;
    }

    public static void AddFriend(int userID, int friendID)
    {
        NetworkData.AddUserFriends.AddFriends(userID, friendID);
    }

    public static int GetUserID(string username)
    { 
        int userID = NetworkData.GetUserID.RetrieveUserID(username);
        return userID;
    }
}
