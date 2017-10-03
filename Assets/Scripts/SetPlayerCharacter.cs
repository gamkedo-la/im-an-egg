using UnityEngine;
using UnityEngine.Assertions;

public class SetPlayerCharacter : MonoBehaviour
{
	[SerializeField] private EggType character;
	[SerializeField] private AnimatedTexture playerTextureAnimator;

	void Start ()
	{
		Assert.IsNotNull( character );
		Assert.IsNotNull( playerTextureAnimator );

		playerTextureAnimator.frames = character.frames;
	}
	
	void Update ()
	{
		
	}
}
