using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTriggerForgetTimes : MonoBehaviour {
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object Entered Trigger");

		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("Main Menu");
	}
}
