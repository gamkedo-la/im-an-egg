using UnityEngine;

public class SetFragmentMaterials : MonoBehaviour
{
	public void SetMaterial( Texture texture )
	{
		var meshes = GetComponentsInChildren<MeshRenderer>( );

		foreach ( var mesh in meshes )
		{
			mesh.material.SetTexture( "_MainTex", texture );
		}
	}
}
