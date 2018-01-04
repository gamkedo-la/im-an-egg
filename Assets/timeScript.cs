using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeScript : MonoBehaviour {
	private Text timerText;
	private int sec, min;

	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text>();
		sec = min = 0;
		StartCoroutine(countTime());
	}
	
	IEnumerator countTime () {
		while(true) {
			sec++;
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
