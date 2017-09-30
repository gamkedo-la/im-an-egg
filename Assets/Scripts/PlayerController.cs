using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed; // force added to movement dir
	public float jumpForce; // force udded for an upward hop
	public float fallTooFarY; // respawn if we fall below this Y coordinate
	public float maxImpactForce; // if we bump too hard we take damage
    public Vector3 centerOfMass; // for a good wobble

	private Vector3 spawnPosition; // starting position for when we respawn
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
		if ((fallTooFarY != 0f) && (rb.position.y < fallTooFarY))
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

		//if (col.relativeVelocity.magnitude > 1.0f) // soft touches are <1 
		//{
		//	Debug.Log("Hit the ground with relative velocity: " + col.relativeVelocity.magnitude);
		//}

		if (maxImpactForce > 0f) // do we care about impact strength?
		{
			float impactForce = Vector3.Dot(col.contacts[0].normal,col.relativeVelocity) * rb.mass;
			if (Mathf.Abs(impactForce) > maxImpactForce)
			{
				Debug.Log("Hit something HARD! Impact force = " + impactForce);
				// FIXME if not a pillow, lose health, etc
			}
		}

	}

	void OnCollisionExit(Collision col){
		if (col.collider.tag == "Floor") {
			grounded = false;
		}
	}

}