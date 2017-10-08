using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public enum movementDirection
	{
		Vertical,
		HorizontalForward,
		HorizontalSideway
	}

	public movementDirection direction;

	//the from(min) and to(max) distance for the platform.
	public float minY;
	public float maxY;

	public float minX;
	public float maxX;

	public float minZ;
	public float maxZ;

	public float speed = 0.1f;

	//the colliders contacting the object
	private Collider[] objects;

	//if the player touv=ch the platform, true
	private bool touched;

	//we are using the sin fucntion for movement. So these are the starting point of sin values. 
	//-1/1 will make the platform start in corner
	public float y = -1f;
	public float x = 4.567821f;
	public float z = 4.567821f;

	//interpolation values
	float xInter;
	float yInter;
	float zInter;

	//scale of the platform
	private float sizeX;
	private float sizeY;
	private float sizeZ;

	//if we want to adjust the center of the object
	public float extraX; 
	public float extraY; 
	public float extraZ;

	public LayerMask tileLayer;

	public bool moveOnWithPlayerTouch;


	// Use this for initialization
	void Start () {
		sizeX = transform.localScale.x;
		sizeY = transform.localScale.y;
		sizeZ = transform.localScale.z;
	}


	// Update is called once per frame
	void Update () {

		Vector3 center = new Vector3 (transform.position.x + extraX, transform.position.y + extraY, transform.position.z + extraZ);
		Vector3 halfExtents = new Vector3 (sizeX/2, sizeY/2, sizeZ/2);

		//returning colliders that are on the moving platform so that it can move along with it
		objects = Physics.OverlapBox( center, halfExtents, Quaternion.identity , tileLayer);


		if (moveOnWithPlayerTouch == false) {


			//for movement, we use the sin movement. We convert the [-1,1] range of sin to range [0,1] using inverse lerp. 
			//Then we use the converted range on lerp for the min and max diatance to cover.

			//for vertical movement
			if (direction == movementDirection.Vertical) {

				yInter = Mathf.InverseLerp (-1f, 1f, Mathf.Sin (y));

				transform.localPosition = new Vector3 (transform.localPosition.x, (float) Mathf.Lerp (minY, maxY, yInter), transform.localPosition.z);

				y += speed * Time.deltaTime;

			}

			//for horizontal movement wrt z axis
			else if (direction == movementDirection.HorizontalForward) {

				zInter = Mathf.InverseLerp (-1f, 1f, Mathf.Sin (z));

				float pos1 = transform.localPosition.z;

				transform.localPosition = new Vector3 ( transform.localPosition.x, transform.localPosition.y , (float) Mathf.Lerp (minZ, maxZ, zInter));

				float pos2 = transform.localPosition.z;

				//for the objects on top of the moving platform to move along, we add the distance covered by the platform
				//from initial to final position per frame rate to objects 
				for (int i = 0; i < objects.Length; i++) {
					objects[i].transform.localPosition = new Vector3 (objects[i].transform.localPosition.x, objects[i].transform.localPosition.y, objects[i].transform.localPosition.z + (pos2 - pos1));

				}

				z += speed * Time.deltaTime;

			}

			//for horizontal movement wrt x axis
			else if (direction == movementDirection.HorizontalSideway) {

				xInter = Mathf.InverseLerp (-1f, 1f, Mathf.Sin (x));

				float pos1 = transform.localPosition.x;

				transform.localPosition = new Vector3 ( (float) Mathf.Lerp (minX, maxX, xInter), transform.localPosition.y , transform.localPosition.z);

				float pos2 = transform.localPosition.x;

				//for the objects on top of the moving platform to move along, we add the distance covered by the platform
				//from initial to final position per frame rate to objects 
				for (int i = 0; i < objects.Length; i++) {
					objects[i].transform.position = new Vector3 (objects[i].transform.position.x + (pos2 - pos1), objects[i].transform.position.y, objects[i].transform.position.z);

				}

				x += speed * Time.deltaTime;

			}

		}



	}

	void OnCollisionEnter(Collision coll){

		if (coll.gameObject.tag == "Player") {
			moveOnWithPlayerTouch = false;
		}

	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		//Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
		Gizmos.DrawWireCube(new Vector3(transform.localPosition.x + extraX,transform.localPosition.y+extraY,transform.localPosition.z+extraZ), new Vector3(sizeX,sizeY,sizeZ));
	}
}
