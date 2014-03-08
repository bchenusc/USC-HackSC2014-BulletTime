using UnityEngine;
using System.Collections;

public class TriggerFiring : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider platform){
		if (platform.CompareTag("Player")){
			this.transform.parent.GetChild (0).GetChild (0).GetComponent<Firing> ().StartFiring ();
		}
	}

	void OnTriggerExit(Collider platform){
		if (platform.CompareTag("Player")){
			this.transform.parent.GetChild (0).GetChild (0).GetComponent<Firing> ().StopFiring ();
		}
	}

}
