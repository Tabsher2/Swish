using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSoundEffect : MonoBehaviour {

	public AudioClip clip1;
	public AudioClip clip2;
	private AudioSource audio;

	private void OnCollisionEnter(Collision collision)
	{
		bool played = false;
		audio = GetComponent<AudioSource> ();
		audio.clip = clip1;

		audio.Play();
	}

	private void OnCollisionStay(Collision collision)
	{
		audio = GetComponent<AudioSource> ();
		audio.clip = clip2;
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
