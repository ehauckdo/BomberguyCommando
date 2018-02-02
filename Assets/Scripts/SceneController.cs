using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public string[] scenes;
	private int currentScene;
	public bool inGame;

	public void NewClick(Vector3 position){
		if (inGame) {
			LevelController levelController = Helper.LoadLevelController ();
			levelController.PlaceBomb (position);
		}
	}

	public void GoToNextScene(){
		// update index of current scene
		currentScene += 1;
		if (currentScene >= scenes.Length) {
			currentScene = 0;
		}
		// effectively load the next scene from index
		SceneManager.LoadScene (scenes[currentScene]);
		if (currentScene > 0) {
			inGame = true;
		} else
			inGame = false;
	}

	public void GoBackToMenu(){
		inGame = false;
		SceneManager.LoadScene ("Menu");
	}
}
