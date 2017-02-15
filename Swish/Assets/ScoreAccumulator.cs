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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (!hitObstacles.Contains(this.name))
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
        if (this.name.Equals("BrickWall(Clone)"))
            hitObstacles.Add("Wall");
        else if (this.name.Equals("Trampoline(Clone)"))
            hitObstacles.Add("Trampoline");
    }

    public static void CalculateScore()
    {
        shotScore = 100;
        if (hitObstacles.Find(x => x.Contains("Rim")) != null)
            swish = false;
        if (hitObstacles.Find(x => x.Contains("Backboard")) != null)
            swish = false;
        if (hitObstacles.Find(x => x.Contains("Wall")) != null)
            shotScore += 150;
        if (hitObstacles.Find(x => x.Contains("Trampoline")) != null)
            shotScore += 200;
        if (swish)
            shotScore *= 2;

        GameController.SetMadeShot(shotScore, swish);

    }

    public static void ResetScore()
    {
        swish = true;
        hitObstacles.Clear();
    }
}
