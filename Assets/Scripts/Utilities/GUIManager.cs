using UnityEngine;
using System.Collections.Generic;

public class GUIManager : Singleton<GUIManager> {
	protected GUIManager() {}

	private Stack<GUIWindow> guiStack;

	void Awake() {
		guiStack = new Stack<GUIWindow>();
	}

	void OnGUI() {
		Stack<GUIWindow> s = new Stack<GUIWindow>(guiStack);

		foreach (GUIWindow gw in s) {
			gw.OnGUI();
		}
	}

	#region helper
	public void popGUI(bool closeSilently = false) {
		GUIWindow gw = guiStack.Pop();
		if (!closeSilently) {
			gw.OnExit();
		}
	}

	public void addGUI(GUIWindow gw) {
		gw.OnEnter();
		guiStack.Push(gw);
	}

	public void clearGUI() {
		foreach(GUIWindow gw in guiStack) {
			gw.OnExit();
		}
		guiStack.Clear();
	}
	#endregion
}

//The below code is an example of how to set up a main menu using the GUIMananger
/*
 * GUI_MainMenu mainMenu = new GUI_MainMenu();
		mainMenu.CB_startButton += delegate() { 
			Debug.Log("Start!");
			AudioManager.Instance.PlaySoundEffect("button_start", Camera.main.gameObject);
			//GUIManager.Instance.popGUI();
		};
		mainMenu.CB_exitButton += delegate() {
			Debug.Log("Exit.");
			Application.Quit();
		};
		mainMenu.CB_muteButton += delegate() {
			Debug.Log("Mute.");
			AudioManager.Instance.Mute();
		};
		GUIManager.Instance.addGUI(mainMenu);
 */
