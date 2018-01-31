using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	AudioClip boom, kill, bonus, damage, toss;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		boom = (AudioClip)Resources.Load ("SFX/damage1");
		kill = (AudioClip)Resources.Load ("SFX/kill1");
		bonus = (AudioClip)Resources.Load ("SFX/bonus1");
		damage = (AudioClip)Resources.Load ("SFX/bonus1");
		toss = (AudioClip)Resources.Load ("SFX/toss1");
		audioSource = (AudioSource)GetComponent<AudioSource> ();
	}
	
	public void Play(string audioFile){
		switch (audioFile) {
		case "boom":
			audioSource.PlayOneShot (boom);
			break;
		case "kill":
			audioSource.PlayOneShot (kill, 0.5f);
			break;
		case "bonus":
			audioSource.PlayOneShot (bonus);
			break;
		case "damage":
			audioSource.PlayOneShot (damage);
			break;
		case "toss":
			audioSource.PlayOneShot (toss);
			break;
		default:
			break;
		}
	}
}
