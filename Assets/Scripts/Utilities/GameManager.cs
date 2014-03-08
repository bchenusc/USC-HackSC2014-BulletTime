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
	#endregion

	void Awake() {
		timeObjects = new LinkedList<TimeTracker>();
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<CharacterController>();
	}

	void Update() {
		float currentPlayerVelocity = playerController.velocity.sqrMagnitude;

		if (timeStopped && !bulletTimeActive && currentPlayerVelocity > minVelocityBuffer) {
			resumeTime();
			timeStopped = false;
		} else if (!timeStopped && currentPlayerVelocity < minVelocityBuffer) {
			stopTime();
			timeStopped = true;
		}

		if (Input.GetKeyDown(KeyCode.Space) && timeStopped && bulletTimeRemaining > 0) {
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

		if (Input.GetKeyUp(KeyCode.Space)) {
			bulletTimeActive = false;
		}
	}

	public void addTimeObject(TimeTracker tt) {
		timeObjects.AddLast(tt);
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

}
