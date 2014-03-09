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
	CharacterController playerController = null;
	#endregion

	#region otherVariables
	LinkedList<TimeTracker> timeObjects = null;
	bool timeStopped = false;
	bool bulletTimeActive = false;
	bool isPlayerDead = false;
	bool rightTriggerHeld = false;
	bool startButtonHeld = false;
	bool selectButtonHeld = false;
	#endregion

	void Awake() {
		timeObjects = new LinkedList<TimeTracker>();
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<CharacterController>();

	}

	void Update() {
		bool playerMoving = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;

		if (timeStopped && !bulletTimeActive && playerMoving) {
			resumeTime();
			timeStopped = false;
		} else if (!timeStopped && (!playerMoving || bulletTimeActive)) {
			stopTime();
			timeStopped = true;
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
			resetTimeObjectsToInitialState();
			// respawn character here
			return;
		}

		// Completely restart level
		bool startButtonDown = OVRGamepadController.GPC_GetButton((int)OVRGamepadController.Button.Back) && !startButtonHeld;
		startButtonHeld = OVRGamepadController.GPC_GetButton((int)OVRGamepadController.Button.Back);
		if (Input.GetKeyDown(KeyCode.T) || startButtonDown) {
			reset();
		}
	}

	void reset() {
		TimerManager.Instance.RemoveAll();
		clearAllTimeObjects();
		Application.LoadLevel(Application.loadedLevel);
	}

	void resetTimeObjectsToInitialState() {
		LinkedList<TimeTracker>.Enumerator current = timeObjects.GetEnumerator();
		LinkedList<TimeTracker>.Enumerator previous = timeObjects.GetEnumerator();

		while (current.MoveNext()) {
			if (current.Current.gameObject.GetComponent<DestroyBullet>()) {
				Destroy(current.Current.gameObject);
				timeObjects.Remove(current.Current);
				current = previous;
			} else {
				current.Current.ResetObject();
			}

			previous = current;
		}

	}

	public void addTimeObject(TimeTracker tt) {
		timeObjects.AddLast(tt);
		if (timeStopped) {
			tt.StopObject();
		}
	}

	public LinkedList<TimeTracker> getTimeObjects() {
		return timeObjects;
	}

	public void removeTimeObject(TimeTracker tt) {
		timeObjects.Remove(tt);
	}

	void clearAllTimeObjects() {
		timeObjects.Clear();
	}

	void stopTime() {
		foreach (TimeTracker tt in timeObjects) {
			tt.StopObject();
		}
	}

	void resumeTime() {
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

	public bool isBulletTimeActive() {
		return bulletTimeActive;
	}

	public void setPlayerDead() {
		isPlayerDead = true;
	}

	public void setPlayerAlive() {
		isPlayerDead = false;
	}

	public bool getPlayerDead() {
		return isPlayerDead;
	}
}
