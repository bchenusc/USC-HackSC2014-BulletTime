using UnityEngine;
using System.Collections;

public class SmashingWall : MonoBehaviour {

	public GameObject LeftWall;
	public GameObject RightWall;

	Vector3 m_leftWallOriginalPos;
	Vector3 m_rightWallOriginalPos;

	public float smashForce;
	public float separateVelocity;

	public enum SmashingWallState {
		Smashing,
		Smashed,
		Opening,
		Opened
	};

	SmashingWallState m_State = SmashingWallState.Opened;

	void Awake () {
		m_leftWallOriginalPos = LeftWall.transform.position;
		m_rightWallOriginalPos = RightWall.transform.position;
	}

	// Use this for initialization
	void Start () {
		m_State = SmashingWallState.Smashing;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		// Wall is smashing, move them towards each other
		if(!GameManager.Instance.isTimeStopped() && m_State == SmashingWallState.Smashing) {
			LeftWall.rigidbody.AddForce(new Vector3(smashForce, 0, 0) * Time.deltaTime);
			RightWall.rigidbody.AddForce(new Vector3(-smashForce, 0, 0) * Time.deltaTime);
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
		if(LeftWall.transform.position.x <= m_leftWallOriginalPos.x &&
		   RightWall.transform.position.x >= m_rightWallOriginalPos.x &&
		   m_State == SmashingWallState.Opening) {
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
		LeftWall.rigidbody.velocity = new Vector3(-separateVelocity, 0, 0) * Time.deltaTime;
		RightWall.rigidbody.velocity = new Vector3(separateVelocity, 0, 0) * Time.deltaTime;
	}

	void SmashWalls() {
		m_State = SmashingWallState.Smashing;
	}
}
