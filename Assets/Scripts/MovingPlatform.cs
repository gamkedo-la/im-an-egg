using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float speed = 0.1f;

	public bool movesWithPlayerTouch;

	//the from(min) and to(max) distance for the platform.
	public Vector3 start;
	public Vector3 end;


	private float startTime;
	private float journeyLength;

	private Collider[] colliders;
	public LayerMask ignoredLayerByCheckColliderBox;
	public Vector3 centerOfCheckColliderBox;
	public Vector3 sizeOfCheckColliderBox;

	private int k=1;

	// Use this for initialization
	void Start () {
		journeyLength = Vector3.Distance(start, end);
	}

	void FixedUpdate(){

		colliders = Physics.OverlapBox( transform.position + centerOfCheckColliderBox , sizeOfCheckColliderBox , Quaternion.identity , ignoredLayerByCheckColliderBox);
	}

	// Update is called once per frame
	void Update () {

		if (movesWithPlayerTouch == false) {

			if (k == 1) {
				startTime = Time.timeSinceLevelLoad;
				k = 0;
			}

			float distCovered = (Time.timeSinceLevelLoad - startTime) * speed;
			float fracJourney = distCovered / journeyLength;

			Vector3 pos1 = transform.localPosition;

			transform.position = Vector3.Lerp(start,end,fracJourney);

			Vector3 pos2 = transform.localPosition;

			int f = 0;
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders [i].gameObject.tag == "PlayerColliders" && f==0) {
					colliders [i].transform.parent.position += (pos2 - pos1);
					f=1;
				} else if (colliders [i].gameObject.tag != "Player" && colliders [i].gameObject.tag != "PlayerColliders"){
					colliders[i].transform.position += (pos2 - pos1);
				}
			}

			if (fracJourney >= 1f) {
				Vector3 temp = start;
				start = end;
				end = temp;
				journeyLength = Vector3.Distance(start, end);
				k = 1;
			}

		}

	}

	void OnCollisionEnter(Collision coll){

		if (coll.gameObject.tag == "Player") {
			movesWithPlayerTouch = false;
		}

	}


	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawWireCube(transform.position + centerOfCheckColliderBox, sizeOfCheckColliderBox * 2);
	}


}
