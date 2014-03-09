using UnityEngine;
using System.Collections;

public class Firing : MonoBehaviour {
	public int projectileSpeed = 30;
	public Rigidbody projectile;

	void Start () {
		StartFiring ();
		StopFiring ();
	}

	void Update () {

	}

	void Fire (){
		Rigidbody clone; 
				clone = Instantiate (projectile, transform.position, Quaternion.identity) as Rigidbody;
				clone.velocity = transform.forward * projectileSpeed;
				
	}

	public void StartFiring(){
		TimerManager.Instance.Add (gameObject.GetInstanceID() + "Gun", Fire, 1, true);
	}

	public void StopFiring(){
		TimerManager.Instance.Remove (gameObject.GetInstanceID () + "Gun");
	}

}
