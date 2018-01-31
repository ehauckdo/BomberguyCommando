using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour {

	void Start () {
		if (Screen.fullScreen)
			Screen.fullScreen = false;

		Debug.Log("Current Resolution: "+Screen.currentResolution.ToString());
		Screen.SetResolution (640, 360, false);

		ArrayList resolutions = new ArrayList();
		resolutions.Add(new CustomResolution(1280, 720)); 
		resolutions.Add(new CustomResolution(640, 360)); 
		resolutions.Add(new CustomResolution(320, 180));

		int currentHeight = Screen.currentResolution.height;
		int currentWidth = Screen.currentResolution.width;

		int setHeight, setWidth;

		for (int index = 0; index < resolutions.Count; index++) {
			setWidth = ((CustomResolution)resolutions [index]).width;
			setHeight = ((CustomResolution)resolutions [index]).height;

			if (setHeight <= currentHeight * 0.8f || setWidth <= currentWidth * 0.8f) {
				// found a good resolution for the window
				Screen.SetResolution(setWidth, setHeight, false);
				return;
			}
		}

		// minimum resolution
		Screen.SetResolution(320, 180, false);
		
	}

	public void ToggleFullscreen(){
		Screen.fullScreen = !Screen.fullScreen;
	}

}

public class CustomResolution{
	public int width, height;
	public CustomResolution(int width, int height){
		this.width = width;
		this.height = height;
	}
}