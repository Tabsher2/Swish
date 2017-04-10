using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkData;
using System;

public class NetworkController : MonoBehaviour {

    public static void SendMissedShot(int turnOwnerId, int turnNo)
    {
        NetworkData.SendShotData.SendMissedShot(turnOwnerId, turnNo);
    }

    public static void SendMadeShot(int player, int turnOwnerId, int userScore, float locationX, float locationZ, int turnNo, float ballX, float ballY, float ballZ)
    {
        NetworkData.SendShotData.SendMadeShot(player, turnOwnerId, userScore, locationX, locationZ, turnNo, ballX, ballY, ballZ);
    }

    public static void AddLetter(int player, int userLetters, int failedShot)
    {
        NetworkData.SendShotData.AddLetter(player, userLetters, failedShot);
    }

    public static void AddSwish(int user)
    {
        NetworkData.UpdateLeaderboards.AddSwish(user);
    }

    public static void UpdateShotStreak(int user, int increment)
    {
        NetworkData.UpdateLeaderboards.UpdateShotStreak(user, increment);
    }

    public static void UpdateGameEnd(int user, int userScore, int result, int userLetters)
    {
        NetworkData.UpdateLeaderboards.UpdateGameEnd(user, userScore, result, userLetters);
    }

    public static void UpdateTime(int user, float gameTime)
    {
        NetworkData.UpdateLeaderboards.UpdateTime(user, gameTime);
    }

    public static void UpdateCopyResult(int result)
    {
        NetworkData.SendShotData.CopyShotResult(result);
    }

    public static NetworkData.ShotData LoadLastShot()
    {
        NetworkData.ShotData shotData = NetworkData.GetShotData.RetrieveGameInfo();
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
}
