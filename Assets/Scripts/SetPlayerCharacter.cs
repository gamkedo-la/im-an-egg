using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

public class SetPlayerCharacter : MonoBehaviour
{
	public AudioClip click;

	private AudioSource clickSource;

	public bool forgetEggPicked = false;

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject canvas;
	[SerializeField] private GameObject eggScreen;
	[SerializeField] private CameraController cameraController;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private EggType[] characters;

	void Start ()
	{
		Assert.IsNotNull( player );
		Assert.IsNotNull( canvas );
		Assert.IsNotNull( eggScreen );
		Assert.IsNotNull( cameraController );
		Assert.IsNotNull( characters );
		Assert.AreNotEqual( characters.Length, 0 );

		clickSource = GetComponent<AudioSource>();

		if(forgetEggPicked == false) { // set this to true in main scene to keep egg picker there
			int lastEggPicked = PlayerPrefs.GetInt("eggStyle", -1);
			if(lastEggPicked != -1) {
				useEggStyle(lastEggPicked);
			}
		}
	}
	
	public void Select(int index)
	{
		clickSource.PlayOneShot(click);
		PlayerPrefs.SetInt("eggStyle", index);
		useEggStyle(index);
	}

	public void useEggStyle(int index) {
		var egg = Instantiate( player );

		PlayerController pcScript = egg.GetComponent<PlayerController>();
		pcScript.SetSpawnPoint(spawnPoint);

		AnimatedTexture playerTextureAnimator = egg.GetComponent<AnimatedTexture>( );
		playerTextureAnimator.frames = characters[index].frames;
		playerTextureAnimator.Refresh( );

		cameraController.gameObject.SetActive( true );
		cameraController.SetPlayer( egg );
		canvas.SetActive( false );
		eggScreen.SetActive( false );
	}
}
