using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeBNT : MonoBehaviour
{

    private static bool trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (PracticeTNT.isTriggered())
        {
            //trigger = true;
            PracticeScoreAccumulator.CalculateScore();
        }
        //Debug.Log(trigger + "bottom");
    }

    public static bool isTriggered()
    {
        return trigger;
    }

    public static void ResetTrigger()
    {
        trigger = false;
    }
}
