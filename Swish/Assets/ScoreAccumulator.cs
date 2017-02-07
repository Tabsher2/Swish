using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAccumulator : MonoBehaviour {

    static List<string> hitObstacles = new List<string>();
    private static bool swish = true;
    private static float shotScore = 0;

	// Use this for initialization
	void Start () {
        shotScore = 0;
        hitObstacles.Clear();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (this.name.Equals("Rim"))
        {
            hitObstacles.Add("Rim");
        }
        
        else if (this.name.Equals("basketball_hoop_main"))
        {
            hitObstacles.Add("Backboard");
        }
    }

    public static void CalculateScore()
    {
        shotScore = 100;
        if (hitObstacles.Find(x => x.Contains("Rim")) != null)
            swish = false;
        if (hitObstacles.Find(x => x.Contains("backboard")) != null)
            swish = false;

        if (swish)
            shotScore *= 2;



        GameController.SetMadeShot(shotScore, swish);

    }
}
