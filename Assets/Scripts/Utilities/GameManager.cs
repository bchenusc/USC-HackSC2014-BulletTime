using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	protected GameManager() {}

	#region defines
	public float minVelocityBuffer = 0.1f;
	public float bulletTimeRemaining = 10.0f;
	#endregion

	#region playerStats
	GameObject player = null;
	#endregion

	#region otherVariables
	List<TimeTracker> timeObjects = null;
	bool timeStopped = false;
	bool bulletTimeActive = false;
	bool isPlayerDead = false;
	bool rightTriggerHeld = false;
	bool startButtonHeld = false;
	bool selectButtonHeld = false;
	bool inputDisabled = false;
	#endregion

	void Awake() {
		timeObjects = new List<TimeTracker>();
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		Debug.Log("Input Enabled: " + !inputDisabled + ", Time Stopped: " + timeStopped);

		if (inputDisabled) {
			return;
		}
		
		bool playerMoving = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;

		if (timeStopped && !bulletTimeActive && playerMoving) {
			resumeTime();
		} else if (!timeStopped && (!playerMoving || bulletTimeActive)) {
			stopTime();
		}

		bool rightTriggerDown = OVRGamepadController.GPC_GetAxis((int)OVRGamepadController.Axis.RightTrigger) > 0 && !rightTriggerHeld;
		bool rightTriggerUp = OVRGamepadController.GPC_GetAxis((int)OVRGamepadController.Axis.RightTrigger) <= 0 && rightTriggerHeld;

		rightTriggerHeld = OVRGamepadController.GPC_GetAxis((int)OVRGamepadController.Axis.RightTrigger) > 0;

		if (!bulletTimeActive && (Input.GetKeyDown(KeyCode.Space) || rightTriggerDown) && bulletTimeRemaining > 0) {
			bulletTimeActive = true;
		}

		if (bulletTimeActive) {
			if (bulletTimeRemaining > 0) {
				bulletTimeRemaining -= Time.deltaTime;
			} else {
				bulletTimeRemaining = 0;
				bulletTimeActive = false;
			}
		}

		if (bulletTimeActive && (Input.GetKeyUp(KeyCode.Space) || rightTriggerUp)) {
			bulletTimeActive = false;
		}

		// Go back to previous checkpoint
		bool selectButtonDown = OVRGamepadController.GPC_GetButton((int)OVRGamepadController.Button.Back) && !selectButtonHeld;
		selectButtonHeld = OVRGamepadController.GPC_GetButton((int)OVRGamepadController.Button.Back);
		if (Input.GetKeyDown(KeyCode.R) || selectButtonDown) {
			player.GetComponent<PlayerScript>().KillCharacter();
			return;
		}

		// Completely restart level
		bool startButtonDown = OVRGamepadController.GPC_GetButton((int)OVRGamepadController.Button.Start) && !startButtonHeld;
		startButtonHeld = OVRGamepadController.GPC_GetButton((int)OVRGamepadController.Button.Start);
		if (Input.GetKeyDown(KeyCode.T) || startButtonDown) {
			reset();
		}
	}

	void OnLevelWasLoaded(int level) {
		player = GameObject.FindGameObjectWithTag("Player");
		timeStopped = false;
	}

	void reset() {
		TimerManager.Instance.RemoveAll();
		clearAllTimeObjects();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void resetTimeObjectsToInitialState() {
		for (int i = timeObjects.Count - 1; i >= 0; i--) {
			if (timeObjects[i].gameObject.GetComponent<DestroyBullet>() != null) {
				timeObjects[i].gameObject.GetComponent<DestroyBullet>().DestroyProjectile();
				timeObjects.RemoveAt(i);
			} else {
				timeObjects[i].ResetObject();
			}
		}
	}

	public void addTimeObject(TimeTracker tt) {
		timeObjects.Add(tt);
		if (timeStopped) {
			tt.StopObject();
		}
	}

	public void removeTimeObject(TimeTracker tt) {
		timeObjects.Remove(tt);
	}

	void clearAllTimeObjects() {
		timeObjects.Clear();
	}

	public void stopTime() {
		timeStopped = true;
		foreach (TimeTracker tt in timeObjects) {
			tt.StopObject();
		}
	}

	public void resumeTime() {
		timeStopped = false;
		foreach (TimeTracker tt in timeObjects) {
			tt.ResumeObject();
		}
	}

	public bool isTimeStopped() {
		return timeStopped;
	}

	public float getBulletTimeRemaining() {
		return bulletTimeRemaining;
	}

	public void setBulletTimeRemaining(float b) {
		bulletTimeRemaining = b;
	}

	public bool isBulletTimeActive() {
		return bulletTimeActive;
	}

	public void setPlayerDead() {
		isPlayerDead = true;
	}

	public void setPlayerAlive() {
		timeStopped = false;
		isPlayerDead = false;
	}

	public bool getPlayerDead() {
		return isPlayerDead;
	}

	public bool isInputDisabled() {
		return inputDisabled;
	}

	public void disableInput() {
		inputDisabled = true;
	}

	public void enableInput() {
		inputDisabled = false;
	}
}
