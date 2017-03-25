using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBallTimeout : MonoBehaviour {

    private float DC = 1.0f;
    private static float touched = 0;
    private static bool deadBall = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        touched += DC * Time.deltaTime;
        if (touched >= 2.0f)
            deadBall = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        touched = 0;
    }

    public static bool IsBallDead()
    {
        return deadBall;
    }

    public static void ResetDeadBall()
    {
        deadBall = false;
        touched = 0;
    }
}
