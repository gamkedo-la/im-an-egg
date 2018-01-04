using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRecordFetcher : MonoBehaviour {
	private Text timerText;
	public string saveTextTime = "Lev1Record";

	// Use this for initialization
	void Start () {
		timerText = GetComponentInChildren<Text>();
		timerText.text = PlayerPrefs.GetString(saveTextTime,"");
	}
}
