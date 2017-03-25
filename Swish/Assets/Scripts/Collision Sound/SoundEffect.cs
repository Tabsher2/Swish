using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

	public AudioClip clip;

	private void OnCollisionEnter(Collision collision)
	{
		AudioSource.PlayClipAtPoint(clip, transform.position);
	}
}