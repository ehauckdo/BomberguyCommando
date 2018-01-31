using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	private SceneController sceneController;

	void Start () {
		sceneController = Helper.LoadGameController();
	}

	void Update () {
		if (Input.anyKeyDown) {
			Camera c = Camera.main;
			Vector3 mousePos = locateClickPosition ();
			sceneController.NewClick(mousePos);
		}
	}

	Vector3 locateClickPosition(){
		Camera c = Camera.main;
		Vector3 mousePos = c.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane));

		float xPosition = Mathf.Round(mousePos.x *10f) / 10f;
		xPosition = Mathf.Ceil (xPosition) - 0.5f;

		float yPosition = Mathf.Round(mousePos.y *10f) / 10f;
		yPosition = Mathf.Ceil (yPosition)- 0.5f;

		//Debug.Log ("mousePos: "+mousePos.ToString()+", Tile position: "+xPosition +","+ yPosition);
		Vector3 newPosition = new Vector3 (xPosition, yPosition, 0f);
		return newPosition;
	}
		
}
