using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.velocity = new Vector3(collision.rigidbody.velocity.x, collision.rigidbody.velocity.y*1.5f, collision.rigidbody.velocity.z);
    }
}
