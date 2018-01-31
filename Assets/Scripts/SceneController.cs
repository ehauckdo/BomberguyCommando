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
		currentScene += 1;
		if (currentScene >= scenes.Length) {
			currentScene = 0;
		}
		// effectively load the next scene
		SceneManager.LoadScene (scenes[currentScene]);
		if (currentScene > 0) {
			inGame = true;
		} else
			inGame = false;
	}
		
	void GameOver (){
		//SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		GoBackToMenu();
	}

	public void GoBackToMenu(){
		inGame = false;
		SceneManager.LoadScene ("Menu");
	}
}
