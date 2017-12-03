using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrans : MonoBehaviour {

	public AudioClip portal;

	private AudioSource portalSource;

	void Awake () 
	{
		transform.SetParent(null); // DontDestroyOnLoad requires object be at root of hierarchy
		DontDestroyOnLoad(gameObject);

		portalSource = GetComponent<AudioSource>();
		portalSource.clip = portal;
	}

	void OnTriggerEnter (Collider other) 
	{
		portalSource.Play();	
		Destroy(gameObject, 4);
	}
}
