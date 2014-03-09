using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Awake() {
		AudioManager.Instance.enabled = true;
		TimerManager.Instance.enabled = true;
		//InputManager.Instance.enabled = true;
		GameManager.Instance.enabled = true;
		//GUIManager.Instance.enabled = true;
	}

	// Use this for initialization
	void Start () {
		Destroy(this);
	}
}
