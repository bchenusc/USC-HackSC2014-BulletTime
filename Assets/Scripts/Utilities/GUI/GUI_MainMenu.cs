using UnityEngine;
using System.Collections;

public class GUI_MainMenu : GUIWindow {
	public Action CB_startButton;
	public Action CB_exitButton;
	public Action CB_muteButton;

	public override void OnEnter () {

	}
	
	public override void OnGUI () {
		if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 100), "START")) {
			if (CB_startButton != null) {
				CB_startButton();
			}
		}

		if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 100, 150, 100), "EXIT")) {
			if (CB_exitButton != null) {
				CB_exitButton();
			}
		}

		if (GUI.Button(new Rect(Screen.width * 0.01f, Screen.height * 0.01f, 50, 50), "Mute")) {
			if (CB_muteButton != null) {
				CB_muteButton();
			}
		}
	}
	
	public override void OnExit() {

	}
}
