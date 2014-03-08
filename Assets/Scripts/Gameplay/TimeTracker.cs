using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]

public class TimeTracker : MonoBehaviour {

	Vector3 oldVelocity = Vector3.zero;
	Vector3 oldAngularVelocity = Vector3.zero;
	bool oldUseGravity = false;
	Rigidbody rBody = null;

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
		rBody.isKinematic = true;
	}

	public void ResumeObject() {
		rBody.isKinematic = false;
		rBody.velocity = oldVelocity;
		rBody.angularVelocity = oldAngularVelocity;
		rBody.useGravity = oldUseGravity;
	}

	void OnDestroy() {
		GameManager.Instance.removeTimeObject(this);
	}
}
