using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTriggerToMenu : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object Entered Trigger");

		LoadMenuScene ();
	}
	
	public void LoadMenuScene () {
		SceneManager.LoadScene("Main Menu");
	}

}
