using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SnapshotManager : MonoBehaviour {

	public AudioMixerSnapshot FadedIn;
	public AudioMixerSnapshot FadedOut;

	void Start () 
	{
		FadedOut.TransitionTo(0.1f);
		FadedIn.TransitionTo(2.0f);	
	}
}
