using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public enum movementDirection
	{
		AlongXAxis,
		AlongYAxis,
		AlongZAxis
	}

	public movementDirection direction;

	public float speed = 0.1f;

	public bool movesWithPlayerTouch;

	//the from(min) and to(max) distance for the platform.
	public float minPosition;
	public float maxPosition;

	//we are using the sin fucntion for movement. So the angles will determine the starting position of platform
	//-1/1 will make the platform start in corner
	public float x = -1f;

	//interpolation values used in lerp
	private float Inter;


	private Collider[] colliders;
	public LayerMask tileLayer;
	public Vector3 forCheckingColliders;
	public Vector3 checkVolume;

	// Use this for initialization
	void Start () {

	}

	void FixedUpdate(){

		colliders = Physics.OverlapBox( transform.position + forCheckingColliders , checkVolume , Quaternion.identity , tileLayer);
	}

	// Update is called once per frame
	void Update () {

		if (movesWithPlayerTouch == false) {

			//for movement, we use the sin movement. We convert the [-1,1] range of sin to range [0,1] using inverse lerp. 
			//Then we use the converted range on lerp for the min and max diatance to cover.
			Inter = Mathf.InverseLerp (-1f, 1f, Mathf.Sin (x));

			Vector3 pos1 = transform.localPosition;

			if (direction == movementDirection.AlongYAxis) {

				transform.position = new Vector3 (transform.position.x, (float) Mathf.Lerp (minPosition, maxPosition,Inter), transform.position.z);

			}

			//for horizontal movement wrt z axis
			else if (direction == movementDirection.AlongZAxis) {

				transform.position = new Vector3 ( transform.position.x, transform.position.y , (float) Mathf.Lerp (minPosition, maxPosition,Inter));

			}

			//for horizontal movement wrt x axis
			else if (direction == movementDirection.AlongXAxis) {

				transform.position = new Vector3 ( (float) Mathf.Lerp (minPosition, maxPosition,Inter), transform.position.y , transform.position.z);

			}

			Vector3 pos2 = transform.localPosition;

			int k = 0;
			for (int i = 0; i < colliders.Length; i++) {


				if (colliders [i].gameObject.tag == "PlayerColliders" && k==0) {
					colliders [i].transform.parent.position += (pos2 - pos1);
					k=1;
				} else if (colliders [i].gameObject.tag != "Player" && colliders [i].gameObject.tag != "PlayerColliders"){
					colliders[i].transform.position += (pos2 - pos1);
				}

			}

			k=0;

			x += speed * Time.deltaTime;

		}

	}

	void OnCollisionEnter(Collision coll){

		if (coll.gameObject.tag == "Player") {
			movesWithPlayerTouch = false;
		}

	}

	void OnCollisionExit(Collision coll){

		if (coll.gameObject.tag == "Player") {
			movesWithPlayerTouch = false;
		}


	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawWireCube(transform.position + forCheckingColliders, checkVolume * 2 );
	}


}
