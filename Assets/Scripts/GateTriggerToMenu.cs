using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTriggerToMenu : MonoBehaviour {

	public AudioClip click;

	private AudioSource clickSource;

	void Start ()
	{
		gameObject.AddComponent<AudioSource>();
		clickSource = GetComponent<AudioSource>();
		clickSource.clip = click;
		transform.SetParent(null);
		DontDestroyOnLoad(gameObject);
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object Entered Trigger, it is of layer: "+other.gameObject.layer);
		Debug.Log("Compare to:" + LayerMask.NameToLayer("Player"));

		if(other.gameObject.layer != LayerMask.NameToLayer("Player")) {
			return; // stop eggshell fragments from ending map
		}

		clickSource.PlayOneShot(click);

		if(timeScript.instance != null) {
			timeScript.instance.checkForNewRecord();
		}

		LoadMenuScene ();
	}
	
	public void LoadMenuScene () {
		SceneManager.LoadScene("Main Menu");
		Destroy(gameObject, 1);
	}

}
