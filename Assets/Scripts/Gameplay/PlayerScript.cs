using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public enum PlayerState {
		Alive,
		Dying,
		Dead
	}

	PlayerState m_State = PlayerState.Alive;

	Checkpoint currentCheckpoint = null;
	Vector3 levelStartPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		levelStartPos = transform.position;
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		// Didn't hit anything, return
		if(body == null) {
			return;
		}
		Debug.Log (body);
		// If the body is a TimeTracker object
		if(body.gameObject.GetComponent<TimeTracker>() || body.gameObject.CompareTag("Ocean")) {
			Debug.Log (body.gameObject);
			KillCharacter();
		}
	}

	public void KillCharacter() {
		if(m_State == PlayerState.Alive) {
			TimerManager.Instance.Add ("RespawnCharacter", RespawnCharacter, 3f, false, -1, null, true);
			TimerManager.Instance.Add ("ResetTransforms", GameManager.Instance.resetTimeObjectsToInitialState, 2.5f, false, -1, null, true);
			m_State = PlayerState.Dying;
			GetComponent<OVRGamepadController>().enabled = false;
			GetComponent<OVRPlayerController>().enabled = false;
			GameManager.Instance.setPlayerDead();
			GameManager.Instance.disableInput();
			GameManager.Instance.stopTime();
		}
	}

	void RespawnCharacter() {
		if (currentCheckpoint) {
			transform.position = currentCheckpoint.transform.position + new Vector3(0, 1.03f, 0);
			GameManager.Instance.setBulletTimeRemaining(currentCheckpoint.getBulletTimeRemaining());
		} else {
			transform.position = levelStartPos;
			GameManager.Instance.setBulletTimeRemaining(10);
		}
		m_State = PlayerState.Alive;
		GetComponent<OVRGamepadController>().enabled = true;
		GetComponent<OVRPlayerController>().enabled = true;
		GetComponent<OVRMainMenu>().resetDeath();
		GameManager.Instance.setPlayerAlive();
		GameManager.Instance.enableInput();
	}

	public void setCheckpoint(Checkpoint checkpoint) {
		currentCheckpoint = checkpoint;
	}
}
