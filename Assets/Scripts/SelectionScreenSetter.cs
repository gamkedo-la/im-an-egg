using UnityEngine;
using UnityEngine.UI;

public class SelectionScreenSetter : MonoBehaviour
{
	public EggType Type;
	public Button Button;

	private AnimatedTexture tex;

	void Start ()
	{
		tex = GetComponent<AnimatedTexture>( );

		if (Type != null && Button != null)
		{
			tex.frames = Type.frames;
			tex.Refresh( );
			Button.GetComponentInChildren<Text>( ).text = Type.Name;
		}
	}
}
