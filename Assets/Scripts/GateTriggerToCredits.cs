using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerToCredits : MonoBehaviour {
	public Transform spawnTransform;
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object Entered Trigger");

		other.transform.parent.transform.position = spawnTransform.position;
	}
}
