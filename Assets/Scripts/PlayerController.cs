using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed; // force added to movement dir
	public float jumpForce; // force udded for an upward hop
	public float fallTooFarY; // respawn if we fall below this Y coordinate
	public float maxImpactForce; // if we bump too hard we take damage
    public Vector3 centerOfMass; // for a good wobble
	public float hitPoints; // health until we crack up
	public float impactDamageRatio; // ImpactForce x this = hitPoint reduction
	public GameObject deathPrefab; // the broken shell bits

	private float startingHitPoints; // remember what we started with
	private bool currentlyDying = false; // don't move while animating shell breaks
	private Vector3 spawnPosition; // starting position for when we respawn
    private bool grounded;	//checking if the egg is grounded
	private Rigidbody rb;
	private Renderer myRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
		spawnPosition = new Vector3();
		spawnPosition = transform.position;
		startingHitPoints = hitPoints;
		myRenderer = GetComponent<Renderer>();
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
			hitPoints = startingHitPoints;
		}

		// check to see if it is time to die
		if (!currentlyDying && (hitPoints <= 0f))
		{
			Debug.Log("We cracked up!");
			// TODO play sound etc
			currentlyDying = true; // so we don't fire multiple times
			myRenderer.enabled = false; // stop drawing the unbroken egg
			if (deathPrefab)
			{
				var go = Instantiate(deathPrefab, transform.position, transform.rotation);
				go.GetComponent<SetFragmentMaterials>( ).SetMaterial( GetComponent<MeshRenderer>( ).material.GetTexture( "_MainTex" ) );
				StartCoroutine(respawnAfterPausing());
			}
		}

		if (currentlyDying) return; // don't move

        rb.AddForce(movement * speed);
    }


	IEnumerator respawnAfterPausing()
	{
		Debug.Log("About to respawn... " + Time.time);
		yield return new WaitForSeconds(5);
		transform.position = spawnPosition;
		currentlyDying = false;
		hitPoints = startingHitPoints;
		myRenderer.enabled = true;
		Debug.Log("Respawning at " + Time.time);
	}


	//checking if the egg is on the Floor or not
	void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Floor") {
			grounded = true;
		}

		if (!currentlyDying && (maxImpactForce > 0f)) // do we care about impact strength?
		{
			float impactForce = Mathf.Abs(Vector3.Dot(col.contacts[0].normal,col.relativeVelocity) * rb.mass);
			if (impactForce > maxImpactForce)
			{
				Debug.Log("Hit something HARD! Impact force = " + impactForce);
				// FIXME if not a pillow, lose health, etc
				hitPoints -= impactForce * impactDamageRatio;
				Debug.Log("hitPoints are now: " + hitPoints);
			}
		}

	}

	void OnCollisionExit(Collision col){
		if (col.collider.tag == "Floor") {
			grounded = false;
		}
	}

}