using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

	public static SceneController LoadSceneController(){
		SceneController gameController = null;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("SceneController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<SceneController> () as SceneController;
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'SceneController' script from Helper script");
		}
		return gameController;
	}

	public static InputController LoadInputController(){
		InputController inputController = null;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("InputController");
		if (gameControllerObject != null) {
			inputController = gameControllerObject.GetComponent<InputController> () as InputController;
		}
		if (inputController == null) {
			Debug.Log ("Cannot find 'InputController' script from Helper script");
		}
		return inputController;
	}

	public static LevelController LoadLevelController(){
		LevelController levelController = null;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("LevelController");
		if (gameControllerObject != null) {
			levelController = gameControllerObject.GetComponent<LevelController> () as LevelController;
		}
		if (levelController == null) {
			Debug.Log ("Cannot find 'LevelController' script from Helper script");
		}
		return levelController;
	}

	public static AudioController LoadAudioController(){
		AudioController audioController = null;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("AudioController");
		if (gameControllerObject != null) {
			audioController = gameControllerObject.GetComponent<AudioController> () as AudioController;
		}
		if (audioController == null) {
			Debug.Log ("Cannot find 'AudioController' script from Helper script");
		}
		return audioController;
	}

	public static DisplayController LoadDisplayController(){
		DisplayController displayController = null;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("DisplayController");
		if (gameControllerObject != null) {
			displayController = gameControllerObject.GetComponent<DisplayController> () as DisplayController;
		}
		if (displayController == null) {
			Debug.Log ("Cannot find 'DisplayController' script from Helper script");
		}
		return displayController;
	}

}
