using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public enum PlayerState {
		Alive,
		Dying,
		Dead
	}

	PlayerState m_State = PlayerState.Alive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(m_State == PlayerState.Dying) {
			if(GetComponent<OVRMainMenu>()) {
				OVRMainMenu m = GetComponent<OVRMainMenu>();
//				m.GUIShowVRVariables();
			}
			//Quaternion b = a.
			//transform.rotation = Quaternion.Slerp(a
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
			if(m_State == PlayerState.Alive) {
				m_State = PlayerState.Dying;
				GetComponent<OVRGamepadController>().enabled = false;
				GetComponent<OVRPlayerController>().enabled = false;
			}
		}
	}
}
