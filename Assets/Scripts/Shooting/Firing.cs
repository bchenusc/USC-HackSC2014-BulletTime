using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {
	public int projectileSpeed = 50;
	public Rigidbody projectile;

	void Start () {
		TimerManager.Instance.Add (gameObject.GetInstanceID() + "Gun", Fire, 1, true);
	}

	void Update () {

	}

	void Fire (){
		Rigidbody clone; 
				clone = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody;
				clone.velocity = transform.TransformDirection (Vector3.forward * projectileSpeed);
				Destroy (clone.gameObject, 5);
	}
}
