using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class Firing : MonoBehaviour {
	public int projectileSpeed = 30;
	public Rigidbody projectile;
	public float rate = 4;

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

		AudioClip ac = AudioManager.Instance.getAudioClip("shootFireBall");
		transform.audio.volume = 0.5f;
		transform.audio.clip = ac;
		transform.audio.PlayOneShot(ac);
				
	}

	public void StartFiring(){
		TimerManager.Instance.Add (gameObject.GetInstanceID() + "Gun", Fire, rate, true);
	}

	public void StopFiring(){
		TimerManager.Instance.Remove (gameObject.GetInstanceID () + "Gun");
	}

}
