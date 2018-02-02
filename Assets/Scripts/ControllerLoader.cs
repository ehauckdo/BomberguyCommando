using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLoader : MonoBehaviour {

	void Start () {
		if(!GameObject.Find("SceneController")){
			GameObject gc = Instantiate (Resources.Load ("Controllers/SceneController", typeof(GameObject))) as GameObject;
			gc.name = "SceneController";
			DontDestroyOnLoad (gc.transform.gameObject);
		}

		if(!GameObject.Find("InputController")){
			GameObject gc = Instantiate (Resources.Load ("Controllers/InputController", typeof(GameObject))) as GameObject;
			gc.name = "InputController";
			DontDestroyOnLoad (gc.transform.gameObject);
		}

		if(!GameObject.Find("AudioController")){
			GameObject gc = Instantiate (Resources.Load ("Controllers/AudioController", typeof(GameObject))) as GameObject;
			gc.name = "AudioController";
			DontDestroyOnLoad (gc.transform.gameObject);
		}

		if(!GameObject.Find("DisplayController")){
			GameObject gc = Instantiate (Resources.Load ("Controllers/DisplayController", typeof(GameObject))) as GameObject;
			gc.name = "DisplayController";
			DontDestroyOnLoad (gc.transform.gameObject);
		}
	}

}
