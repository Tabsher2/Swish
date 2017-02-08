﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopNetTrigger : MonoBehaviour {

    private static bool trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!BottomNetTrigger.isTriggered())
        {
            //SHOT MADE HERE
            trigger = true;
        }
        //Debug.Log(trigger + "top");
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
