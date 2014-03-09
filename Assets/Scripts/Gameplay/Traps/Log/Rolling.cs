using UnityEngine;
using System.Collections;

public class Rolling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Roll(){
		Vector3 direction = this.transform.parent.GetChild (1).position - transform.position;
		direction = new Vector3 (direction.x, 0, direction.z);
		direction = Vector3.Normalize (direction);
		this.gameObject.GetComponent<Rigidbody>().AddForce(direction * 8000);
	}
}
