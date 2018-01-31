using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

	public float secondsToExplode;

	private GameObject generatedExplosion;
	private GameObject generatedBomb;
	private AudioController audioController;

	private bool wrongPlacement;

	// Use this for initialization
	void Start () {
		Vector3 playerPosition = new Vector3(0f, -3f, 0f);
		generatedBomb = Instantiate (Resources.Load ("BombSprite", typeof(GameObject)), playerPosition, transform.rotation) as GameObject;
		generatedBomb.transform.parent = this.transform;
		StartCoroutine (MoveObjectTo(generatedBomb, transform.position, GetDistance() * 0.1f));
		StartCoroutine (StartExplosionCount ());
		audioController = Helper.LoadAudioController ();
	}

	IEnumerator MoveObjectTo (GameObject objectToMove, Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;
		do {
			objectToMove.transform.position = Vector3.Lerp (startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		} while (elapsedTime < seconds);	
		objectToMove.transform.position = end;

		/*
			// Prototype code to make the bomb move if there is an enemy in the tile
		if (wrongPlacement) {
			Vector3 currentPosition = transform.position;
			currentPosition.y += 1;
			wrongPlacement = false;
			StartCoroutine (MoveObjectTo(gameObject, currentPosition, 0.5f));
		}*/

	}

	IEnumerator StartExplosionCount(){
		yield return new WaitForSeconds (secondsToExplode);
		Explode ();

		yield return new WaitForSeconds (0.2f);
		ExplosionController explosion = generatedExplosion.GetComponent<ExplosionController> ();
		explosion.DisableColliders ();
		//BoxCollider2D[] collider = generatedExplosion.GetComponents<BoxCollider2D>();
		//collider [0].enabled = false;
		//collider [1].enabled = false;

		yield return new WaitForSeconds (0.5f);

		Destroy (generatedExplosion);
		gameObject.SetActive (false);
	}

	void Explode(){
		Destroy (generatedBomb);
		audioController.Play ("boom");
		generatedExplosion = Instantiate (Resources.Load ("Explosion", typeof(GameObject)), transform) as GameObject;
		generatedExplosion.transform.parent = this.transform;
		Helper.LoadLevelController ().BombExploded();
	}

	float GetDistance(){
		float distance = Vector3.Distance (transform.position, generatedBomb.transform.position);
		return distance;
	}

	/*void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("COLLISION HAPPENED");
		wrongPlacement = true;
	}*/
}
