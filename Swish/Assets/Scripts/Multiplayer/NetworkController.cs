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

    public static void SendMadeShot(int player, int turnOwnerId, float userScore, float locationX, float locationZ, int turnNo, float ballX, float ballY, float ballZ)
    {
        NetworkData.SendShotData.SendMadeShot(player, turnOwnerId, userScore, locationX, locationZ, turnNo, ballX, ballY, ballZ);
    }

    public static void AddLetter(int player, int userLetters)
    {
        NetworkData.SendShotData.AddLetter(player, userLetters);
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
}
