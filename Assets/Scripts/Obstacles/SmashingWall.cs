using UnityEngine;
using System.Collections;

public class SmashingWall : MonoBehaviour {

	public GameObject LeftWall;
	public GameObject RightWall;

	public float smashForce;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		LeftWall.rigidbody.AddForce(new Vector3(smashForce, 0, 0) * Time.deltaTime);
		RightWall.rigidbody.AddForce(new Vector3(-smashForce, 0, 0) * Time.deltaTime);
	}
}
