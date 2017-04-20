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
    Vector3 swipeDistance;
    const int factor = 325;

    int throwDirection;

    Vector3 worldStartPos;
    Vector3 screenStartPos;

    private void OnMouseDown()
    {
        startTime = Time.time;
        worldStartPos = Input.mousePosition;
        screenStartPos = Input.mousePosition;
        throwDirection = (int)CameraController.GetCameraDirection();
        //worldStartPos.z = transform.position.z - Camera.main.transform.position.z;
        worldStartPos.x = transform.position.x - Camera.main.transform.position.x;
        worldStartPos = Camera.main.ScreenToWorldPoint(worldStartPos);
    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        Vector3 endPos = Input.mousePosition;
        swipeDistance = (endPos - screenStartPos);
        //endPos.x = transform.position.z - Camera.main.transform.position.z;
        //endPos.z = transform.position.x - Camera.main.transform.position.x;
        //endPos = Camera.main.ScreenToWorldPoint(endPos);
        swipeTime = endTime - startTime;
        float swipeSpeed = swipeDistance.magnitude / swipeTime;
        if (swipeTime < maxTime && swipeDistance.magnitude > minSwipeDist)
        {
            if (!isThrown && !GameController.disableThrow)
            {
                isThrown = true;
                rb.useGravity = true;
                Vector3 force = new Vector3(0, 0, 0);
                float angleDif = Mathf.Atan(swipeDistance.x/swipeDistance.y) * Mathf.Rad2Deg;
                float forceAngle = CameraController.GetCameraDirection() + angleDif;
                float yAngle = LocationSelector.GetYangle();
                force.z = Mathf.Cos(forceAngle * Mathf.Deg2Rad) * swipeDistance.magnitude;
                force.x = Mathf.Sin(forceAngle * Mathf.Deg2Rad) * swipeDistance.magnitude;
                Vector2 xzMagnitude = new Vector2(force.x, force.z);
                force.y = swipeDistance.magnitude * 0.8f;
                float oldY = force.y;
                force.y += Mathf.Tan(yAngle * Mathf.Deg2Rad) * -1 * xzMagnitude.magnitude;
                float ratio = oldY / force.y;
                xzMagnitude *= ratio;
                float newwXZMag = force.y/(Mathf.Tan(yAngle * Mathf.Deg2Rad) * -1);
                force.z = xzMagnitude.y;
                force.x = xzMagnitude.x;
                force /= factor;
                //force /= 1.5f;
                force *= swipeSpeed / 250;

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
