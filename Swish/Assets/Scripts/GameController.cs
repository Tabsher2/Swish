using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject Basketball;
    public static Vector3 ballStart = new Vector3(3f, 1f, 0f);

    public static bool throughBasket = false;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            Instantiate(Basketball, ballStart, Quaternion.identity);

    }

    public void CreateBall()
    {
        Instantiate(Basketball, ballStart, Quaternion.identity);
    }

    public void OnTriggerExit(Collider other)
    {
        throughBasket = true;
    }
}
