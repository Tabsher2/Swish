using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomNetTrigger : MonoBehaviour {

    private static bool trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (TopNetTrigger.isTriggered())
        {
            //trigger = true;
            ScoreAccumulator.CalculateScore();
        }
        //Debug.Log(trigger + "bottom");
    }

    public static bool isTriggered()
    {
        return trigger; 
    }

    public static void resetTrigger()
    {
        trigger = false;
    }
}
