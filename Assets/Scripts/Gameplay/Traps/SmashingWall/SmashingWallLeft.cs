using UnityEngine;
using System.Collections;

public class SmashingWallLeft : MonoBehaviour {
	
	public GameObject RightWall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject == RightWall) {
			transform.parent.gameObject.GetComponent<SmashingWall>().HasCollided();
		}
	}
}
