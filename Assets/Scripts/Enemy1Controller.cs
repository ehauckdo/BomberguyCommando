using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour {

	public static readonly int DOWN = 0;
	public static readonly int UP = 1;
	public static readonly int RIGHT = 2;
	public static readonly int LEFT = 3;

	public Sprite idle, dying;
	public float movingTime;

	private LevelController levelController;
	private AudioController audioController;
	private bool moving;
	private bool duringMove;
	private bool setToDestroy;

	// Use this for initialization
	void Start () {
		levelController = Helper.LoadLevelController ();
		audioController = Helper.LoadAudioController ();
	}

	// Update is called once per frame
	void Update () {

		if (moving == false) {
			Vector3 nextPosition = GetNextPosition();
			StartCoroutine (MoveObjectTo (gameObject, nextPosition, movingTime));
		}

		//Debug.DrawLine (transform.position, transform.position +Vector3.up);
		//Debug.DrawLine (transform.position, transform.position +Vector3.down);
		Debug.DrawRay (transform.position, Vector2.down*1.5f);
		Debug.DrawRay (transform.position, Vector2.right*1.5f);
		Debug.DrawRay (transform.position, Vector2.left*1.5f);
			
	}

	IEnumerator MoveObjectTo (GameObject objectToMove, Vector3 end, float seconds)
	{
		moving = true;
		duringMove = true;
		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;
		do {
			objectToMove.transform.position = Vector3.Lerp (startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		} while (elapsedTime < seconds && duringMove == true);

		yield return new WaitForSeconds (0.5f);

		if(duringMove == false)
			objectToMove.transform.position = startingPos;
		else
			objectToMove.transform.position = end;

		duringMove = false;
		moving = false;

	}

	Vector3 GetNextPosition(){
		Vector3 nextPosition = transform.position;
		ArrayList probabilities = new ArrayList ();

		if (!checkRaycastHit (Vector2.down))
			for (int i = 0; i < 78; i++)
				probabilities.Add (DOWN);
		if (!checkRaycastHit (Vector2.left)) 
			for (int i = 0; i < 10; i++)
				probabilities.Add (LEFT);
		if (!checkRaycastHit (Vector2.right)) 
			for (int i = 0; i < 10; i++)
				probabilities.Add (RIGHT);
		if (!checkRaycastHit (Vector2.up))
			for (int i = 0; i < 2; i++)
				probabilities.Add (UP);

		/*string probs = "";
		for (int i = 0; i < probabilities.Count; i++)
			probs += ((int)probabilities[i]) + ", ";
		Debug.Log ("probabilities: " + probs);*/

		if (probabilities.Count == 0) {
			//Debug.Log ("Cant move to any directions!!!!!");
			return nextPosition;
		} else {
			int result = (int) probabilities [Random.Range (0, probabilities.Count)];

			if (result == RIGHT) nextPosition.x += 1; 
			else if (result == LEFT) nextPosition.x -= 1;
			else if (result == DOWN) nextPosition.y -= 1;
			else if (result == UP)   nextPosition.y += 1;

			return nextPosition;
		}
	}

	bool checkRaycastHit(Vector2 direction){
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
		if (hit.collider != null && (hit.collider.tag == "Boundary" || hit.collider.tag == "Bomb" || hit.collider.tag == "Block" || hit.collider.tag == "TopBoundary")) {
			//Debug.Log ("Collision with " + hit.collider.tag);
			return true;
		} else
			return false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Boundary")) {
			//Debug.Log ("Collision with boundary!");
			duringMove = false;
		} else if (other.CompareTag ("Bomb")) {
			duringMove = false;
		} else if (other.CompareTag ("BottomBoundary")) {
				Destroy (gameObject);
				levelController.PlayerGotHit ();
				levelController.EnemyKilled ();
				
		} else if (other.CompareTag ("Explosion")) {
			if (setToDestroy == false) {
				levelController.UpdateScore (10);

				setToDestroy = true;
				StartCoroutine (PlayDeathAnimation ());
			}
		}
			
	}

	IEnumerator PlayDeathAnimation(){
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = dying;
		audioController.Play ("kill");
		yield return new WaitForSeconds (0.3f);
		levelController.EnemyKilled();
		Destroy (gameObject);
	}

}
