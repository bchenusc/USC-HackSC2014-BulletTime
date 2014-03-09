using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TimerManager.Instance.Add (gameObject.GetInstanceID () + "Destroy", DestroyProjectile, 5, false);

		AudioClip ac = AudioManager.Instance.getAudioClip("fireBall");
		transform.audio.loop = true;
		transform.audio.clip = ac;
		transform.audio.Play();
	}

	public void DestroyProjectile() {
		TimerManager.Instance.Remove (gameObject.GetInstanceID () + "Destroy");
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.GetComponent<DestroyBullet>() == null){
			DestroyProjectile();
		}
	}

}
