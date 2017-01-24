using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour {

    public Rigidbody ball;

    public float throwForce;
    public Vector3 startPosition = new Vector3 ( 5.9f, 0.9f, 0.01f );

	// Use this for initialization
	void Start () {
        ball = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        if (ball.position.x > 10)
            ball.position = startPosition;
		
	}

    public void Throw()
    {
        ball.AddForce(new Vector3(200, 300, 0));
    }
}
