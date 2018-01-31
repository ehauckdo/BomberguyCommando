using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject explosionCenter = null;
		explosionCenter = getChildObject ("center");

		foreach (Transform child in transform) {
			//Debug.Log (child.gameObject.name+", "+child.gameObject.transform.position);
			Vector3 position = child.gameObject.transform.position;

			if (child.gameObject.name != "center") {
				//float distance = GetDistance (child.gameObject, explosionCenter);
				//Debug.Log ("Distance between center and " + child.gameObject.name + ": " + distance);

				//if(checkRaycastHit(child.gameObject.transform.position, explosionCenter.transform.position)){
				//	Debug.Log ("There is a block between center and " + child.gameObject.name);
				//}

				//Debug.Log("Moving from "+ child.gameObject.name+" to center");
				if (CheckObstacles (child.gameObject.transform.position, explosionCenter.transform.position, 1f)) {
					//Debug.Log (child.gameObject.name + " set to false");
					child.gameObject.SetActive (false);
				}
			}

			/*if (IsThereBlockCollision (position)) {
				Debug.Log("Disabled "+child.gameObject.name);
				child.gameObject.SetActive (false);
			}*/
		}

		/*if (explosionCenter != null) {
			checkRaycastHit (explosionCenter.transform.position, Vector2.down);
		}*/
	}

	public void DisableColliders(){
		foreach (Transform child in transform) {
			Collider2D collider = (Collider2D) child.gameObject.GetComponent<Collider2D> ();
			collider.enabled = false;
		}
	}

	GameObject getChildObject(string name){
		foreach (Transform child in transform) {
			//Debug.Log (child.gameObject.name+", "+child.gameObject.transform.position);
			if (child.gameObject.name == name)
				return child.gameObject;
		}
		return null;
	}

	bool checkRaycastHit(Vector2 origin, Vector2 direction){
		RaycastHit2D hit = Physics2D.Raycast(origin, direction, Vector2.Distance(origin,direction));
		if (hit.collider != null && (hit.collider.tag == "Boundary" || hit.collider.tag == "Bomb" || hit.collider.tag == "Block" || hit.collider.tag == "TopBoundary")) {
			//Debug.Log ("Collision with " + hit.collider.tag);
			return true;
		} else
			return false;
	}

	bool IsThereBlockCollision(Vector3 position){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (new Vector2(position.x, position.y), 0);
		for(int i = 0; i < hitColliders.Length; i++){
			//Debug.Log ("Collision: " + hitColliders [0].gameObject.name);
			if (hitColliders [i].gameObject.tag == "Block" || hitColliders [i].gameObject.tag == "BottomBoundary" || hitColliders [i].gameObject.tag == "TopBoundary")
				return true;
		} 
		return false;
	}

	float GetDistance(GameObject obj1, GameObject obj2){
		Collider2D obj1Collider = (Collider2D) obj1.GetComponent<Collider2D> ();
		Collider2D obj2Collider = (Collider2D) obj2.GetComponent<Collider2D> ();
		return Physics2D.Distance (obj1Collider, obj2Collider).distance;
	}

	bool CheckObstacles(Vector3 origin, Vector3 destiny, float tileSize){
		//Debug.Log ("Initial Pos: " + origin.ToString()+ ", Center Pos: "+destiny.ToString());
		Vector3 newPos = origin;
		for (int i = 0; i < 2; i++) {

			if(IsThereBlockCollision(newPos)){
				//Debug.Log("There is a collision in this point");
				return true;
			}

			newPos = Vector3.MoveTowards (newPos, destiny, tileSize);
			//Debug.Log ("New Pos: " + newPos.ToString());
			if (Vector3.Equals (newPos, destiny))
				break;
		}
		return false;
	}
}
