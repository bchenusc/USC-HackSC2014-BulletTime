using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * How to use:
 * 1. 
 * 
 * Notes:
 * 1. 
 * 
 * @ Matthew Pohlmann
 * 
*/

public class InputManager : Singleton<InputManager> {
	protected InputManager() {}

	#region actions
	private Action OnLeftMouseButtonDown;
	private Action OnRightMouseButtonDown;
	private Action OnKeyPressed;
	private Action OnKeyHeld;
	#endregion
	
	// Update is called once per frame
	void Update () {
		if (OnLeftMouseButtonDown != null && Input.GetMouseButtonDown(0))
			OnLeftMouseButtonDown();
		if (OnRightMouseButtonDown != null && Input.GetMouseButtonDown(1))
			OnRightMouseButtonDown();
		if (OnKeyPressed != null && Input.anyKeyDown)
			OnKeyPressed();
		if (OnKeyHeld != null && Input.anyKey)
			OnKeyHeld();
	}

	#region register functions
	public void RegisterOnLeftMouseButtonDown(Action a) {
		OnLeftMouseButtonDown += a;
	}

	public void RegisterOnRightMouseButtonDown(Action a) {
		OnRightMouseButtonDown += a;
	}

	public void RegisterOnKeyPressed(Action a) {
		OnKeyPressed += a;
	}

	public void RegisterOnKeyHeld(Action a) {
		OnKeyHeld += a;
	}
	#endregion

	#region deregister functions
	public void DeregisterOnLeftMouseButtonDown(Action a) {
		OnLeftMouseButtonDown -= a;
	}
	
	public void DeregisterOnRightMouseButtonDown(Action a) {
		OnRightMouseButtonDown -= a;
	}

	public void DeregisterOnKeyPressed(Action a) {
		OnKeyPressed -= a;
	}

	public void DeregisterOnKeyHeld(Action a) {
		OnKeyHeld -= a;
	}
	#endregion

	//private class 
}
