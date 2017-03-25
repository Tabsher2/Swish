using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeBall : MonoBehaviour {

    public GameObject player;

    public float maxTime;
    public float minSwipeDist;

    float startTime;
    float endTime;
    float swipeDistance;
    float swipeTime;

    Vector3 startPos;
    Vector3 endPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;

                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = endTime - startTime;

                if (swipeTime < maxTime && swipeDistance > minSwipeDist)
                {
                    swipe();
                }


            }
        }
	}

    void swipe()
    {
        Vector2 distance = endPos - startPos;
        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            //Horizontal Swipe. We don't want none of that.
        }
        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            //Vertical 
            Debug.Log("we are on our way");
            if (distance.y > 0)
            {
                player.GetComponent<BallThrow>().Throw();
            }
        }
    }
}
