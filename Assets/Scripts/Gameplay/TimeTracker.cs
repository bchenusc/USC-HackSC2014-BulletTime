using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]

public class TimeTracker : MonoBehaviour {

	SaveState initialState;

	Vector3 oldVelocity = Vector3.zero;
	Vector3 oldAngularVelocity = Vector3.zero;
	bool oldUseGravity = false;
	bool oldIsKinematic = false;
	Rigidbody rBody = null;

	bool applicationQuitting = false;

	// Use this for initialization
	void Start () {
		rBody = gameObject.GetComponent<Rigidbody>();
		GameManager.Instance.addTimeObject(this);

		oldVelocity = rBody.velocity;
		oldAngularVelocity = rBody.angularVelocity;
		oldUseGravity = rBody.useGravity;
		oldIsKinematic = rBody.isKinematic;

		initialState.velocity = rBody.velocity;
		initialState.angularVelocity = rBody.angularVelocity;
		initialState.useGravity = rBody.useGravity;
		initialState.isKinematic = rBody.isKinematic;
		initialState.position = transform.position;
		initialState.rotation = transform.rotation;
	}

	public void StopObject() {
		oldVelocity = rBody.velocity;
		oldAngularVelocity = rBody.angularVelocity;
		oldUseGravity = rBody.useGravity;
		oldIsKinematic = rBody.isKinematic;

		if (!rBody.isKinematic) {
			rBody.velocity = Vector3.zero;
			rBody.angularVelocity = Vector3.zero;
			rBody.useGravity = false;
		}
		rBody.isKinematic = true;
	}

	public void ResumeObject() {
		rBody.isKinematic = oldIsKinematic;

		if (!rBody.isKinematic) {
			rBody.velocity = oldVelocity;
			rBody.angularVelocity = oldAngularVelocity;
			rBody.useGravity = oldUseGravity;
		}
	}

	public void ResetObject() {
		rBody.isKinematic = false;

		rBody.velocity = initialState.velocity;
		rBody.angularVelocity = initialState.angularVelocity;
		rBody.useGravity = initialState.useGravity;
		transform.position = initialState.position;
		transform.rotation = initialState.rotation;
		rBody.isKinematic = initialState.isKinematic;
	}

	void OnApplicationQuit() {
		applicationQuitting = true;
	}

	void OnDestroy() {
		if (!applicationQuitting) {
			GameManager.Instance.removeTimeObject(this);
		}
	}
}

struct SaveState {
	public Vector3 position;
	public Vector3 velocity;
	public Vector3 angularVelocity;
	public bool useGravity;
	public bool isKinematic;
	public Quaternion rotation;
}
