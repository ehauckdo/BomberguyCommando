using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	public Text startGame;
	public Text toggleFullscreen;
	public Text exit;
	private SceneController sceneController;

	public void StartGame(){
		sceneController = Helper.LoadSceneController ();
		sceneController.GoToNextScene ();
		UpdateFullscreenToggleText ();
	}
		
	public void ExitGame(){
		Application.Quit ();
	}

	public void ToggleFullscreen(){
		DisplayController display = Helper.LoadDisplayController();
		display.ToggleFullscreen ();
		UpdateFullscreenToggleText ();
	}

	public void UpdateFullscreenToggleText(){
		if (Screen.fullScreen)
			toggleFullscreen.text = "Windowed Mode";
		else 
			toggleFullscreen.text = "Fullscreen Mode";
	}
		
}
