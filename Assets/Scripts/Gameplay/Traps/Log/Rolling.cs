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
		this.gameObject.GetComponent<Rigidbody>().AddForce(0,0,3000);
	}
}
