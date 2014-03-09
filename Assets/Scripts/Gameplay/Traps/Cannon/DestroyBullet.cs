using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TimerManager.Instance.Add (gameObject.GetInstanceID () + "Destroy", DestroyProjectile, 5, false);
	}

	void DestroyProjectile() {
		Destroy (gameObject);
	}

}
