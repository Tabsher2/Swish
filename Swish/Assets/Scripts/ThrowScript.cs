using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    public Rigidbody rb;

    public static float maxTime = 1;
    public static float minSwipeDist = 25;
    private static bool isThrown = false;

    float startTime;
    float endTime;
    float swipeTime;
    float swipeDistance;
    float factor = GameController.ballStart.x / 2.8f;

    Vector3 startPos;
    Vector3 startPosition;

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        startPosition = Input.mousePosition;
        startPos.x = transform.position.x - Camera.main.transform.position.x;
        startPos = Camera.main.ScreenToWorldPoint(startPos);


    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        Vector3 endPos = Input.mousePosition;
        swipeDistance = (endPos - startPosition).magnitude;
        endPos.z = transform.position.x - Camera.main.transform.position.x;
        endPos = Camera.main.ScreenToWorldPoint(endPos);
        swipeTime = endTime - startTime;
        if (swipeTime < maxTime && swipeDistance > minSwipeDist)
        {
            if (!isThrown)
            {
                isThrown = true;
                rb.useGravity = true;
                Vector3 force = endPos - startPos;
                force.x = force.magnitude;
                force.y = force.magnitude;
                force /= swipeTime;

                rb.velocity = force;
            }
        }
    }


    IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(4.0f);
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    public static void ResetThrow()
    {
        isThrown = false;
    }
}
