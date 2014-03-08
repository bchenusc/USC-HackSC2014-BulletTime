using UnityEngine;
using System.Collections;

public class TriggerRolling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider platform){
		if (platform.CompareTag("Player")){
			this.transform.parent.GetChild (0).GetComponent<Rolling>().Roll();
		}
	}
	
	void OnTriggerExit(Collider platform){
		if (platform.CompareTag("Player")){

		}
	}

}
