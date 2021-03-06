using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	//[Header("Dependencies")]
	//[SerializeField, Tooltip("The player object.")] private GameObject player;

	[Header("Parameters")]
	[SerializeField, Tooltip("How smooth the camera movement will be (lower = smoother).")] private float smoothing = 0.5f;
	[SerializeField, Tooltip("Position relative to the player.")] private Vector3 offset = new Vector3(0, 2.5f, -9.5f);
	[SerializeField, Tooltip("Camera tilt in the X axis.")] private float angleX = 15.0f;

	private Vector3 positionVelocity;
	private GameObject player;

	void Start( )
	{
		//Assert.IsNotNull( player );

		transform.Rotate( angleX, 0, 0 );
	}

	void LateUpdate( ) // using LateUpdate so it happens after all movement code
	{
		if (player != null)
			FollowPlayer( );
	}

	public void SetPlayer(GameObject player)
	{
		this.player = player;
	}

	private void FollowPlayer( )
	{
		Vector3 newPosition = player.transform.position + offset;
		RaycastHit rhInfo;
		if(Physics.Raycast(player.transform.position, offset, out rhInfo, offset.magnitude, ~LayerMask.GetMask("Player"))) {
			newPosition = rhInfo.point + 2.0f * Vector3.up;
			// Debug.Log(rhInfo.collider.gameObject.name);
		}
		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionVelocity, smoothing);
	}
}
