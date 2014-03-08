using UnityEngine;
using System.Collections;

public class TimeTracker : MonoBehaviour {
	
	//State = 1 means moving
	//State = 0 means not moving
	int m_state = 1;
	int m_lerpFactor = 0;
	bool m_objStopped = false;


	public int State{
		set { m_state = value;}
		get { return m_state;}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (m_objStopped) {
			Mathf.Lerp
		}
	
	}

	void StopObject(bool shouldStop){
		m_objStopped = shouldStop;
	}
}
