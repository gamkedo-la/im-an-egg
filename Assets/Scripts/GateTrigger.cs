using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTrigger : MonoBehaviour {

	public string sceneToLoad = "Main Menu";

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object Entered Trigger");

		//sceneName = sceneToLoad.name;

		LoadScene ();
	}

	public void LoadScene () {
		SceneManager.LoadScene(sceneToLoad);
	}
	
	

}
