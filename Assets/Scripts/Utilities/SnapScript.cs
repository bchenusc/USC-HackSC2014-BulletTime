using UnityEngine;
using System.Collections;

public class SnapScript : MonoBehaviour {
	public bool RoundToHalf = false;

	// Use this for initialization
	void Awake () {
		if (RoundToHalf) {
			transform.position = new Vector3(RoundTo(transform.position.x, 0.5f), RoundTo(transform.position.y, 0.5f), 0);
		} else {
			transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
		}
	}

	float RoundTo(float num, float roundTo) {
		return Mathf.Round(num / roundTo) * roundTo;
	}
}
