using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]

public class TimeTracker : MonoBehaviour {
	
	//State = 1 means moving
	//State = 0 means not moving
	/*int m_state = 1;
	int m_lerpFactor = 0;
	bool m_objStopped = false;*/
	Vector3 oldVelocity = Vector3.zero;
	Vector3 oldAngularVelocity = Vector3.zero;
	bool oldUseGravity = false;
	Rigidbody rBody = null;


	/*public int State{
		set { m_state = value;}
		get { return m_state;}
	}*/
	void Awake () {

	}

	// Use this for initialization
	void Start () {
		rBody = gameObject.GetComponent<Rigidbody>();
		GameManager.Instance.addTimeObject(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StopObject() {
		oldVelocity = rBody.velocity;
		oldAngularVelocity = rBody.angularVelocity;
		oldUseGravity = rBody.useGravity;
		rBody.velocity = Vector3.zero;
		rBody.angularVelocity = Vector3.zero;
		rBody.useGravity = false;
	}

	public void ResumeObject() {
		rBody.velocity = oldVelocity;
		rBody.angularVelocity = oldAngularVelocity;
		rBody.useGravity = oldUseGravity;
	}
}
