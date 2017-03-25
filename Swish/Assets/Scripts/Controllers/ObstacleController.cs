using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    //Obstacle Starting Location
    public static Vector3 wallStart = new Vector3(6.08f, 1.92f, -1.37f);
    public static Vector3 trampStart = new Vector3(6f, 0.11f, 0f);

    //Obstacles

    public GameObject wallObstacle;
    private GameObject newWall;

    public GameObject trampolineObstacle;
    private GameObject newTrampoline;


    

    public void CreateWall()
    {
        newWall = Instantiate(wallObstacle, wallStart, Quaternion.Euler(0, 90, 0));
        newWall.SetActive(true);
    }

    public void CreateTrampoline()
    {
        newTrampoline = Instantiate(trampolineObstacle, trampStart, Quaternion.identity);
        newTrampoline.SetActive(true);
    }
}
