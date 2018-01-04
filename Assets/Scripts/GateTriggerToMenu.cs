using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateTriggerToMenu : MonoBehaviour {

	public AudioClip click;

	private AudioSource clickSource;

	void Start ()
	{
		clickSource = GetComponent<AudioSource>();
		transform.SetParent(null);
		DontDestroyOnLoad(gameObject);
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Object Entered Trigger");

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
