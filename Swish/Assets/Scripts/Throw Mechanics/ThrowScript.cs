using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    public Rigidbody rb;

    public static float maxTime = 1;
    public static float minSwipeDist = 25;
    public static bool isThrown = false;

    public static Vector3 shotVelocity;

    float startTime;
    float endTime;
    float swipeTime;
    float swipeDistance;
    float factor = GameController.ballStart.x / 2.8f;

    int throwDirection;

    Vector3 startPos;
    Vector3 startPosition;

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        startPosition = Input.mousePosition;
        throwDirection = (int)CameraController.GetCameraDirection();
        switch (throwDirection)
        {
            case 0:
                startPos.z = transform.position.z - Camera.main.transform.position.z;
                break;
            case 90:
                startPos.x = transform.position.x - Camera.main.transform.position.x;
                break;
            case 180:
                startPos.z = transform.position.z - Camera.main.transform.position.z;
                break;
        }
        startPos = Camera.main.ScreenToWorldPoint(startPos);
    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        Vector3 endPos = Input.mousePosition;
        swipeDistance = (endPos - startPosition).magnitude;
        switch (throwDirection)
        {
            case 0:
                endPos.x = transform.position.z - Camera.main.transform.position.z;
                break;
            case 90:
                endPos.z = transform.position.x - Camera.main.transform.position.x;
                break;
            case 180:
                endPos.x = transform.position.z - Camera.main.transform.position.z;
                break;
        }
        endPos = Camera.main.ScreenToWorldPoint(endPos);
        Debug.Log(endPos);
        swipeTime = endTime - startTime;
        if (swipeTime < maxTime && swipeDistance > minSwipeDist)
        {
            if (!isThrown)
            {
                isThrown = true;
                rb.useGravity = true;
                Vector3 force = endPos - startPos;
                switch (throwDirection)
                {
                    case 0:
                        force.z = force.magnitude;
                        break;
                    case 90:
                        force.x = force.magnitude;
                        break;
                    case 180:
                        force.z = -force.magnitude;
                        break;
                    case 270:
                        break;
                }
                force.y = force.magnitude;
                force /= swipeTime;

                rb.velocity = force;
                shotVelocity = rb.velocity;
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
        shotVelocity = new Vector3(0, 0, 0);
    }
}
