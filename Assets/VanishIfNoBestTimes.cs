using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishIfNoBestTimes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Lev1Record" + "sec", -1) == -1 &&
		   PlayerPrefs.GetInt("Lev2Record" + "sec", -1) == -1 &&
		   PlayerPrefs.GetInt("Lev3Record" + "sec", -1) == -1 &&
		   PlayerPrefs.GetInt("Lev4Record" + "sec", -1) == -1) {
			Destroy(gameObject);
		}
	}

}
