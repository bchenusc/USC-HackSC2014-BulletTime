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
		foreach(ContactPoint contact in collision.contacts) {
			if(contact.otherCollider.gameObject == RightWall) {
				transform.parent.gameObject.GetComponent<SmashingWall>().HasCollided();
			}
		}
	}
}
