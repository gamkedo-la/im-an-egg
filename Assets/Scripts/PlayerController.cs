using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
	public float jumpForce;
	public float fallTooFarY; // respawn if we fall below this Y coordinate
    public Vector3 centerOfMass;

	private Vector3 spawnPosition;

    private bool grounded;	//checking if the egg is grounded
	private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
		spawnPosition = new Vector3();
		spawnPosition = transform.position;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		//For jumping
		if (Input.GetButtonDown ("Jump") && grounded == true) {
			movement.y = jumpForce;				
		}
        
		// ensure we don't fall forever
		if ((fallTooFarY != 0) && (rb.position.y < fallTooFarY))
		{
			Debug.Log("We fell out of the world! Respawning.");
			// TODO: die? play sound etc
			transform.position = spawnPosition;
		}

        rb.AddForce(movement * speed);
    }


	//checking if the egg is on the Floor or not
	void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Floor") {
			grounded = true;
		}
	}

	void OnCollisionExit(Collision col){
		if (col.collider.tag == "Floor") {
			grounded = false;
		}
	}

}