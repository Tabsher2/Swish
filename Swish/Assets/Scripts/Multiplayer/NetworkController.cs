using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkData;
using System;

public class NetworkController : MonoBehaviour {

    public static void SendMissedShot(string turnOwnerId, int turnNo)
    {
        NetworkData.SendShotData.SendMissedShot(turnOwnerId, turnNo);
    }

    public static void SendMadeShot(int player, string turnOwnerId, int userScore, float locationX, float locationZ, int turnNo, float ballX, float ballY, float ballZ)
    {
        NetworkData.SendShotData.SendMadeShot(player, turnOwnerId, userScore, locationX, locationZ, turnNo, ballX, ballY, ballZ);
    }

    public static void AddLetter(int player, int userLetters, int failedShot)
    {
        NetworkData.SendShotData.AddLetter(player, userLetters, failedShot);
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

    public static NetworkData.UserData FetchUserData(string user, string opponent)
    {
        NetworkData.UserData userData = NetworkData.FetchUserData.RetrieveUserInfo(user, opponent);
        return userData;
    }

    public static NetworkData.AccountInfo FetchAccountInfo(string user)
    {
        NetworkData.AccountInfo userAccountInfo = NetworkData.FetchAccountInfo.RetrieveUserAccountInfo(user);
        return userAccountInfo;
    }
}
