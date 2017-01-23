using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour {

    public float factor;
    public float startTime;
    public Vector3 startPos;
    public Rigidbody rb;

    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        startPos.x = transform.position.x - Camera.main.transform.position.x;
        startPos = Camera.main.ScreenToWorldPoint(startPos);


    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
        Vector3 endPos = Input.mousePosition;
        endPos.z = transform.position.x - Camera.main.transform.position.x;
        endPos = Camera.main.ScreenToWorldPoint(endPos);
       
        Vector3 force = endPos - startPos;
        force.x = force.magnitude;
        force.y = force.magnitude;
        force /= (Time.time - startTime);
        
        rb.velocity = (force * factor);
        

    }

    IEnumerator ReturnBall()
    {
        yield return new WaitForSeconds(4.0f);
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
}
