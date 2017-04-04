using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSoundEffect : MonoBehaviour {

	public AudioClip clip1;
	public AudioClip clip2;
	private AudioSource audio;


	private void OnCollisionEnter(Collision collision)
	{
		audio = GetComponent<AudioSource> ();
		bool played = false;
		if (!audio.isPlaying) 
		{ 
			audio.clip = clip1;
			audio.Play ();
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		audio = GetComponent<AudioSource> ();
		if (!audio.isPlaying) 
		{
			audio.clip = clip2;
			audio.Play ();
		}
	}
}
