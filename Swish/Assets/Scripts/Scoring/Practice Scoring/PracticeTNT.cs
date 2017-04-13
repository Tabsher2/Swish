using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTNT : MonoBehaviour
{	
	private AudioSource audio;
    private static bool trigger;
	public AudioClip clip;
	
    private void OnTriggerEnter(Collider other)
    {
        if (!PracticeBNT.isTriggered())
        {
            //SHOT MADE HERE
			audio = GetComponent<AudioSource> ();
			audio.Play();
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
