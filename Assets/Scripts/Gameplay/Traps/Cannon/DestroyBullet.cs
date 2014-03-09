using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TimerManager.Instance.Add (gameObject.GetInstanceID () + "Destroy", DestroyProjectile, 5, false);
	}

	public void DestroyProjectile() {
		TimerManager.Instance.Remove (gameObject.GetInstanceID () + "Destroy");
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision c){
		DestroyProjectile();
	}

}
