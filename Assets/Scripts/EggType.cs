using System.Collections.Generic;
using UnityEngine;

public enum EggTypeT
{
	SirEgbertRollington,
	GreenAlien,
}

[CreateAssetMenu()]
public class EggType : ScriptableObject
{
	public string Name;
	public List<AnimatedTexture.AnimatedTextureFrame> frames = new List<AnimatedTexture.AnimatedTextureFrame>();
}
