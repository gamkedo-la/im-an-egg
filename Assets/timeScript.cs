using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeScript : MonoBehaviour {
	private Text timerText;
	private int sec, min;
	public string saveKey = "Lev1Record";
	private int totalSec;
	public static timeScript instance;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text>();
		sec = min = 0;
		totalSec = 0;
		StartCoroutine(countTime());
		instance = this;
	}

	public void checkForNewRecord() {
		int recordSec = PlayerPrefs.GetInt(saveKey+"sec",999999);
		if(recordSec > instance.totalSec) {
			PlayerPrefs.SetInt(saveKey+"sec",totalSec);
			PlayerPrefs.SetString(saveKey,timerText.text);
		}
		Destroy(instance.gameObject);
		instance = null;
	}
	
	IEnumerator countTime () {
		while(true) {
			sec++;
			totalSec++;

			if(sec >= 60) {
				sec -= 60;
				min++;
			}
			string minText, secText;
			if(min == 0) {
				minText = "00";
			} else if(min < 10) {
				minText = "0"+min;
			} else {
				minText = ""+min;
			}

			if(sec == 0) {
				secText = "00";
			} else if(sec < 10) {
				secText = "0"+sec;
			} else {
				secText = ""+sec;
			}

			timerText.text = minText+":"+secText;
			yield return new WaitForSeconds(1.0f);
		}
	}
}
