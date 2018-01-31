using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public SceneController sceneController;

	public Text startGame;
	public Text toggleFullscreen;
	public Text exit;

	public void StartGame(){
		//Debug.Log ("Starting game");
		sceneController = Helper.LoadGameController ();
		sceneController.GoToNextScene ();
		UpdateFullscreenToggleText ();
	}

	public void ToggleFullscreen(){
		DisplayController display = Helper.LoadDisplayController();
		display.ToggleFullscreen ();
		UpdateFullscreenToggleText ();
	}

	public void ExitGame(){
		//Debug.Log ("Closing game");
		Application.Quit ();
	}

	public void UpdateFullscreenToggleText(){
		if (Screen.fullScreen)
			toggleFullscreen.text = "Windowed Mode";
		else 
			toggleFullscreen.text = "Fullscreen Mode";
	}
		
}
