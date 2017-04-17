using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{

    int tapCount;
    float doubleTapTimer;
    public static bool allowDeletion = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
        }
        if (tapCount > 0)
        {
            doubleTapTimer += Time.deltaTime;
        }
        if (doubleTapTimer > 0.5f)
        {
            doubleTapTimer = 0f;
            tapCount = 0;
        }
        if (tapCount >= 2 && allowDeletion)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == this.GetComponent<Collider>())
                {
                    Destroy(this.gameObject);
                    ObstacleController.placedObstacles.Remove(this.gameObject);
                }
            }
            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
    }

    public static void DeleteAllObstacles()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("BrickWall");
        for (int i = 0; i < walls.Length; i++)
        {
            Destroy(walls[i]);
        }

        GameObject[] trampolines = GameObject.FindGameObjectsWithTag("Trampoline");
        for (int i = 0; i < trampolines.Length; i++)
        {
            Destroy(trampolines[i]);
        }
    }
}
