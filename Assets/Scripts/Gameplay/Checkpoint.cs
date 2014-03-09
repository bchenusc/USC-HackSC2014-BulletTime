using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public bool resetsBulletTime = false;
	float bulletTimeRemaining = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
			if (bulletTimeRemaining < 0) {
				other.GetComponent<PlayerScript>().setCheckpoint(this);
				if (resetsBulletTime) {
					bulletTimeRemaining = 10;
				} else {
					bulletTimeRemaining = GameManager.Instance.getBulletTimeRemaining();
				}
			}
		}
	}

	public float getBulletTimeRemaining() {
		return bulletTimeRemaining;
	}
}
