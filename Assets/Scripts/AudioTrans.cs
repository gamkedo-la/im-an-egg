using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrans : MonoBehaviour {

	public AudioClip portal;

	private AudioSource portalSource;

	void Start () 
	{
		portalSource = GetComponent<AudioSource>();
		DontDestroyOnLoad(gameObject);
	}

	void OnTriggerEnter (Collider other) 
	{
		portalSource.PlayOneShot(portal);	
	}
}
