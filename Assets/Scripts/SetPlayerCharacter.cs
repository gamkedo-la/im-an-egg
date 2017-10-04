using UnityEngine;
using UnityEngine.Assertions;

public class SetPlayerCharacter : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject canvas;
	[SerializeField] private CameraController cameraController;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private EggType[] characters;

	void Start ()
	{
		Assert.IsNotNull( player );
		Assert.IsNotNull( canvas );
		Assert.IsNotNull( cameraController );
		Assert.IsNotNull( characters );
		Assert.AreNotEqual( characters.Length, 0 );
	}
	
	public void Select(int index)
	{
		var egg = Instantiate( player, spawnPoint.position, Quaternion.identity );

		AnimatedTexture playerTextureAnimator = egg.GetComponent<AnimatedTexture>( );
		playerTextureAnimator.frames = characters[index].frames;
		playerTextureAnimator.Refresh( );

		cameraController.SetPlayer( egg );
		canvas.SetActive( false );
	}
}
