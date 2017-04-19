using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAccumulator : MonoBehaviour
{
    [Serializable]
    public class Obstacle
    {
        public int obstacleID;
        public float locationX;
        public float locationY;
        public float locationZ;

        public Obstacle(int id, float x, float y, float z)
        {
            this.obstacleID = id;
            this.locationX = x;
            this.locationY = y;
            this.locationZ = z;
        }
    }

    public static List<Obstacle> hitObstacles = new List<Obstacle>();
    private static bool swish = true;
    private static int shotScore = 0;

    // Use this for initialization
    void Start()
    {
        shotScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        int tempy = 0;
        switch (this.name)
        {
            case "Rim":
                tempy = 0;
                break;
            case "basketball_hoop_main":
                tempy = -1;
                break;
            case "BrickWall(Clone)":
                tempy = 1;
                break;
            case "Trampoline(Clone)":
                tempy = 2;
                break;
        }
        hitObstacles.Add(new Obstacle(tempy, this.transform.position.x, this.transform.position.y, this.transform.position.z));
    }

    public static void CalculateScore()
    {
        shotScore = 100;
        bool removeRim = false;
        List<int> rimIndices = new List<int>();
        bool removeBackboard = false;
        List<int> bbIndices = new List<int>();
        for (int i = 0; i < hitObstacles.Count; i++)
        {
            switch (hitObstacles[i].obstacleID)
            {
                case 0:
                    swish = false;
                    removeRim = true;
                    rimIndices.Add(i);
                    break;
                case -1:
                    swish = false;
                    removeBackboard = true;
                    bbIndices.Add(i);
                    break;
                case 1:
                    shotScore += 150;
                    break;
                case 2:
                    shotScore += 200;
                    break;
            }
        }
        if (removeRim)
        {
            for (int i = 0; i < rimIndices.Count; i++)
                hitObstacles.RemoveAt(rimIndices[i]);
        }
        if (removeBackboard)
        {
            for (int i = 0; i < bbIndices.Count; i++)
                hitObstacles.RemoveAt(bbIndices[i]);
        }
        if (GameController.ballStart.x < -6)
            shotScore += 100;
        if (GameController.ballStart.x < 0)
            shotScore += 100;
        if (swish)
            shotScore *= 2;
        GameController.SetMadeShot(shotScore, swish);

    }

    public static void ResetScore()
    {
        swish = true;
        hitObstacles.Clear();
    }

    public static List<Obstacle> GetUsedObstacles()
    {
        return hitObstacles;
    }
}
