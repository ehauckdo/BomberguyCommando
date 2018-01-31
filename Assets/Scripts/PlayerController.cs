using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Sprite idle, throwing;
	AudioController audioController;

	public void Start(){
		audioController = Helper.LoadAudioController ();
	}

	public void ThrowBomb(){
		audioController.Play ("toss");
		StopAllCoroutines ();
		StartCoroutine (PlayThrowBombAnimation ());
	}

	IEnumerator PlayThrowBombAnimation(){
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = throwing;
		yield return new WaitForSeconds (0.3f);
		spriteRenderer.sprite = idle;
	}
}
