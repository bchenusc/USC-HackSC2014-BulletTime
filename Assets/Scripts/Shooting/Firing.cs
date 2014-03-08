using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {
	public int projectileSpeed = 50;
	public Rigidbody projectile;

	void Start () {

	}

	void Update () {
		Fire ();
	}

	void Fire (){
		Rigidbody clone; 
		if (Input.GetKeyDown(KeyCode.R)) {
				clone = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
				clone.velocity = transform.TransformDirection (Vector3.forward * projectileSpeed);
				Destroy (clone.gameObject, 5);
		}
	}
}
