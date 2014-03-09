using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	protected GameManager() {}

	#region const defines
	public float minVelocityBuffer = 0.1f;
	#endregion

	#region playerStats
	GameObject player = null;
	CharacterController playerController = null;
	#endregion

	#region otherVariables
	LinkedList<TimeTracker> timeObjects = null;
	bool timeStopped = false;
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

		if (timeStopped && currentPlayerVelocity > minVelocityBuffer) {
			resumeTime();
			timeStopped = false;
		} else if (!timeStopped && currentPlayerVelocity < minVelocityBuffer) {
			stopTime();
			timeStopped = true;
		}
	}

	public void addTimeObject(TimeTracker tt) {
		timeObjects.AddLast(tt);
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

}
