﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEggShell : MonoBehaviour {

	public AudioClip[] eggShell;
	public AudioMixerGroup output;

	public float minPitch = .85f;
	public float maxPitch = 1.15f;
	public float minVol = .85f;
	public float maxVol = 1.15f;

	public bool alreadyPlayed = false;

	void OnCollisionEnter()
	{
		if (alreadyPlayed = false) 
		{
			PlayShellPiece ();
			alreadyPlayed = true;
		}
	}

	void PlayShellPiece()
	{
		int randomClip = Random.Range (0, eggShell.Length);
		AudioSource shellSource = gameObject.AddComponent<AudioSource>();
		shellSource.clip = eggShell [randomClip];
		shellSource.outputAudioMixerGroup = output;
		shellSource.pitch = Random.Range (minPitch, maxPitch);
		shellSource.volume = Random.Range (minVol, maxVol);
		shellSource.Play();
		Destroy (shellSource, eggShell[randomClip].length);
	}
}