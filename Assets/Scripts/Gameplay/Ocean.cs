using UnityEngine;
using System.Collections;

public class Ocean : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
			other.GetComponent<PlayerScript>().KillCharacter();
		}
	}
}
