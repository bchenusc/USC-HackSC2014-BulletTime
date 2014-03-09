﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public enum PlayerState {
		Alive,
		Dying,
		Dead
	}

	PlayerState m_State = PlayerState.Alive;

	public GameObject[] Checkpoints;
	Vector3 currentCheckpoint;

	// Use this for initialization
	void Start () {
		if(Checkpoints.Length > 0) {
			currentCheckpoint = Checkpoints[0].transform.position;
		} else {
			currentCheckpoint = transform.position;
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		// Didn't hit anything, return
		if(body == null) {
			return;
		}
		// If the body is a TimeTracker object
		if(body.gameObject.GetComponent<TimeTracker>()) {
			KillCharacter();
		}
	}

	public void KillCharacter() {
		if(m_State == PlayerState.Alive) {
			TimerManager.Instance.Add ("RespawnCharacter", RespawnCharacter, 3f, false);
			m_State = PlayerState.Dying;
			GetComponent<OVRGamepadController>().enabled = false;
			GetComponent<OVRPlayerController>().enabled = false;
			GameManager.Instance.setPlayerDead();
		}
	}

	void RespawnCharacter() {
		transform.position = currentCheckpoint + new Vector3(0, 1.03f, 0);
		m_State = PlayerState.Alive;
		GetComponent<OVRGamepadController>().enabled = true;
		GetComponent<OVRPlayerController>().enabled = true;
		GetComponent<OVRMainMenu>().resetDeath();
		GameManager.Instance.setPlayerAlive();
	}

	public void setCheckpoint(GameObject checkpoint) {
		currentCheckpoint = checkpoint.transform.position;
	}
}
