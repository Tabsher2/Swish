using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour
{

    //Obstacle Starting Location
    public static Vector3 wallStart = new Vector3(6.08f, 1.92f, -1.37f);
    public static Vector3 trampStart = new Vector3(6f, 0.11f, 0f);

    //Obstacles
    private Collider[] overlaps;
    public GameObject obstacleMenu;
    public GameObject wallObstacle;
    private GameObject newWall;
    private GameObject newObstacle;
    public GameObject trampolineObstacle;
    private GameObject newTrampoline;

    public static List<GameObject> placedObstacles = new List<GameObject>();

    //Banner
    public GameObject bannerPanel;
    public GameObject bannerButton;
    public GameObject deleteOkay;
    public Text bannerText;

    //Placement Selection
    Vector3 clickPos;
    private bool overlapFlag = false;
    public static Vector3 obstacleLocation;
    private bool obstacleDown = false;
    private bool placeWall = false;
    private bool placeTramp = false;
    public static bool loadObstacles = false;
    int currentMessage = 1;
    int t = 0;

    private void Update()
    {
        if (loadObstacles)
        {
            PlaceObstaclesOnMap();
            loadObstacles = false;
        }

        if (DestroyObstacle.allowDeletion)
        {
            t++;
            if (t == 90)
            {
                t = 0;
                SwitchMessage();
            }
        }
    }

    public void CreateWall()
    {
        if (placedObstacles.Count < 30)
        {
            obstacleMenu.SetActive(false);
            bannerButton.SetActive(false);
            bannerText.alignment = TextAnchor.MiddleCenter;
            bannerText.text = "Tap on the court to select a location.";
            bannerPanel.SetActive(true);
            placeWall = true;
        }

    }

    public void CreateTrampoline()
    {
        if (placedObstacles.Count < 30)
        {
            obstacleMenu.SetActive(false);
            bannerButton.SetActive(false);
            bannerText.alignment = TextAnchor.MiddleCenter;
            bannerText.text = "Tap on the court to select a location.";
            bannerPanel.SetActive(true);
            placeTramp = true;
        }

    }

    private void OnMouseDown()
    {
        if (obstacleDown)
            Destroy(newObstacle);

        if (placeWall)
        {
            clickPos = Input.mousePosition;
            double aspectRatio = (double)Screen.width / (double)Screen.height;
            float ourX;
            float ourZ;
            //16x9
            if (aspectRatio <= 0.58)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.386f * Screen.width)) * 5;
            }
            //5x3
            else if (aspectRatio <= 0.615)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.364f * Screen.width)) * 5;
            }
            //8x5
            else if (aspectRatio <= 0.641)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.349f * Screen.width)) * 5;
            }
            //3x2
            else if (aspectRatio <= 0.708)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.325f * Screen.width)) * 5;
            }
            //4x3
            else
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.29f * Screen.width)) * 5;
            }
            obstacleLocation = new Vector3(ourX, 1.92f, ourZ);
            overlaps = Physics.OverlapSphere(obstacleLocation, 2.0f);
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].name == ("Rim") || overlaps[i].name == ("PreviousLocation(Clone)") || overlaps[i].name == ("BrickWall(Clone)") || overlaps[i].name == ("Trampoline(Clone)"))
                    overlapFlag = true;
            }
            if (!overlapFlag)
            {
                newObstacle = Instantiate(wallObstacle, obstacleLocation, Quaternion.Euler(0, 90, 0));
                obstacleDown = true;
                bannerPanel.SetActive(true);
                bannerButton.SetActive(true);
                bannerText.alignment = TextAnchor.MiddleLeft;
                bannerText.text = "Place obstacle here?";
            }
            else
            {
                Array.Clear(overlaps, 0, overlaps.Length);
                bannerButton.SetActive(false);
                bannerText.alignment = TextAnchor.MiddleCenter;
                bannerText.text = "Cannot place obstacle there.";
            }
            overlapFlag = false;
        }

        else if (placeTramp)
        {
            clickPos = Input.mousePosition;
            double aspectRatio = (double)Screen.width / (double)Screen.height;
            float ourX;
            float ourZ;
            //16x9
            if (aspectRatio <= 0.58)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.386f * Screen.width)) * 5;
            }
            //5x3
            else if (aspectRatio <= 0.615)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.364f * Screen.width)) * 5;
            }
            //8x5
            else if (aspectRatio <= 0.641)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.349f * Screen.width)) * 5;
            }
            //3x2
            else if (aspectRatio <= 0.708)
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.325f * Screen.width)) * 5;
            }
            //4x3
            else
            {
                clickPos.x -= (Screen.width / 2);
                clickPos.y -= (Screen.height / 2);
                ourX = ((clickPos.y / (0.435f * Screen.height)) * 10) - 0.044f;
                ourZ = -(clickPos.x / (0.29f * Screen.width)) * 5;
            }
            obstacleLocation = new Vector3(ourX, 0.11f, ourZ);
            overlaps = Physics.OverlapSphere(obstacleLocation, 1f);
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].name == ("Rim") || overlaps[i].name == ("PreviousLocation(Clone)") || overlaps[i].name == ("BrickWall(Clone)") || overlaps[i].name == ("Trampoline(Clone)"))
                    overlapFlag = true;
            }
            if (!overlapFlag)
            {
                newObstacle = Instantiate(trampolineObstacle, obstacleLocation, Quaternion.identity);

                obstacleDown = true;
                bannerPanel.SetActive(true);
                bannerButton.SetActive(true);
                bannerText.alignment = TextAnchor.MiddleLeft;
                bannerText.text = "Place obstacle here?";

            }
            else
            {
                Array.Clear(overlaps, 0, overlaps.Length);
                bannerButton.SetActive(false);
                bannerText.alignment = TextAnchor.MiddleCenter;
                bannerText.text = "Cannot place obstacle there.";
            }
            overlapFlag = false;

        }

    }

    public void FinalizeObstacle()
    {
        Destroy(newObstacle);
        if (placeWall)
        {
            newWall = Instantiate(wallObstacle, new Vector3(obstacleLocation.x, 1.92f, obstacleLocation.z), Quaternion.Euler(0, 90, 0));
            placedObstacles.Add(newWall);
            obstacleMenu.SetActive(true);
            placeWall = false;
        }
        else if (placeTramp)
        {
            newTrampoline = Instantiate(trampolineObstacle, new Vector3(obstacleLocation.x, 0.11f, obstacleLocation.z), Quaternion.identity);
            placedObstacles.Add(newTrampoline);
            obstacleMenu.SetActive(true);
            placeTramp = false;
        }
        bannerButton.SetActive(false);
        bannerPanel.SetActive(false);
    }

    public static List<ScoreAccumulator.Obstacle> GetPlacedObstacles()
    {
        List<ScoreAccumulator.Obstacle> finalObstacles = new List<ScoreAccumulator.Obstacle>();
        for (int i = 0; i < placedObstacles.Count; i++)
        {
            int temp = 0;
            switch (placedObstacles[i].name)
            {
                case "BrickWall(Clone)":
                    temp = 1;
                    break;
                case "Trampoline(Clone)":
                    temp = 2;
                    break;
            }
            finalObstacles.Add(new ScoreAccumulator.Obstacle(temp, placedObstacles[i].transform.position.x, placedObstacles[i].transform.position.y, placedObstacles[i].transform.position.z));
        }
        return finalObstacles;
    }

    public void PlaceObstaclesOnMap()
    {
        for (int i = 0; i < GameController.mapObstacles.Count; i++)
        {
            switch (GameController.mapObstacles[i].obstacleID)
            {
                case 1:
                    newWall = Instantiate(wallObstacle, new Vector3(GameController.mapObstacles[i].locationX, GameController.mapObstacles[i].locationY, GameController.mapObstacles[i].locationZ), Quaternion.Euler(0, 90, 0));
                    placedObstacles.Add(newWall);
                    break;
                case 2:
                    newTrampoline = Instantiate(trampolineObstacle, new Vector3(GameController.mapObstacles[i].locationX, GameController.mapObstacles[i].locationY, GameController.mapObstacles[i].locationZ), Quaternion.identity);
                    placedObstacles.Add(newTrampoline);
                    break;
            }
        }
    }

    public void AllowObstacleDeletion()
    {
        obstacleMenu.SetActive(false);
        bannerText.text = "Double Tap to Delete.";
        deleteOkay.SetActive(true);
        bannerPanel.SetActive(true);
        DestroyObstacle.allowDeletion = true;
    }

    public void SwitchMessage()
    {
        if (currentMessage == 1)
        {
            bannerText.alignment = TextAnchor.MiddleLeft;
            bannerText.text = "Double Tap to Delete.";
            currentMessage = 2;
        }
        else
        {
            bannerText.alignment = TextAnchor.MiddleLeft;
            bannerText.text = "Press Okay when finished.";
            currentMessage = 1;
        }
    }

    public void StopDeletingObstacles()
    {
        bannerPanel.SetActive(false);
        deleteOkay.SetActive(false);
        obstacleMenu.SetActive(true);
        DestroyObstacle.allowDeletion = false;
    }
}
