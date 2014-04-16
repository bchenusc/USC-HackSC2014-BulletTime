using UnityEngine;
using System.Collections;

public class SmashingWall : MonoBehaviour {

	public GameObject LeftWall;
	public GameObject RightWall;

	Vector3 m_leftWallOriginalPos;
	Vector3 m_rightWallOriginalPos;

	public float smashForce;
	public float separateVelocity;

	Vector3 smashDirection;

	public enum SmashingWallState {
		Smashing,
		Smashed,
		Opening,
		Opened
	};

	SmashingWallState m_State = SmashingWallState.Opened;

	void Awake () {
	}

	// Use this for initialization
	void Start () {
		m_leftWallOriginalPos = LeftWall.transform.position;
		m_rightWallOriginalPos = RightWall.transform.position;

		LeftWall.transform.LookAt(m_rightWallOriginalPos);
		RightWall.transform.LookAt(m_leftWallOriginalPos);

		m_State = SmashingWallState.Smashing;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		// Wall is smashing, move them towards each other
		if(!GameManager.Instance.isTimeStopped() && m_State == SmashingWallState.Smashing) {
			LeftWall.rigidbody.AddForce(LeftWall.transform.forward.normalized * 500 * Time.deltaTime);
			RightWall.rigidbody.AddForce(RightWall.transform.forward.normalized * 500 * Time.deltaTime);
		// Wall has smashed, freeze all movement
		} else if(m_State == SmashingWallState.Smashed || m_State == SmashingWallState.Opened) {
			if(!LeftWall.rigidbody.isKinematic) {
				LeftWall.rigidbody.velocity = Vector3.zero;
			}
			if(!RightWall.rigidbody.isKinematic) {
				RightWall.rigidbody.velocity = Vector3.zero;
			}
		// Opening the walls, make them separate at a constant velocity
		} else if(!GameManager.Instance.isTimeStopped() && m_State == SmashingWallState.Opening) {

		}
		// If the walls reach their original positions while opening, set their initial position,
		// change the state, and set a timer to smash the walls again
		Vector3 dirToTarget = m_leftWallOriginalPos - LeftWall.transform.position;
		dirToTarget = dirToTarget.normalized;
		float dotProd = Vector3.Dot(LeftWall.transform.forward, dirToTarget);
		bool doneMoving = Mathf.RoundToInt(dotProd) == 1;

		if(m_State == SmashingWallState.Opening && doneMoving) {
			LeftWall.transform.position = m_leftWallOriginalPos;
			RightWall.transform.position = m_rightWallOriginalPos;
			m_State = SmashingWallState.Opened;
			TimerManager.Instance.Add ("SmashWalls", SmashWalls, 1f, false);
		}
	}

	public void HasCollided() {
		if(m_State == SmashingWallState.Smashing) {
			m_State = SmashingWallState.Smashed;
			TimerManager.Instance.Add ("SeparateWall", SeparateWalls, 1f, false);
		}
	}

	void SeparateWalls() {
		m_State = SmashingWallState.Opening;
		if(!LeftWall.rigidbody.isKinematic) {
			LeftWall.rigidbody.velocity = LeftWall.transform.forward * -separateVelocity * Time.deltaTime;
		}
		if(!RightWall.rigidbody.isKinematic) {
			RightWall.rigidbody.velocity = RightWall.transform.forward * -separateVelocity * Time.deltaTime;
		}
	}

	void SmashWalls() {
		m_State = SmashingWallState.Smashing;
	}
}
