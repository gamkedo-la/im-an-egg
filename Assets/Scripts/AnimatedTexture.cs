using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour {

    [System.Serializable]
    public class AnimatedTextureFrame
    {
        public Texture texture;
        public float duation;
    }
    public List<AnimatedTextureFrame> frames = new List<AnimatedTextureFrame>();
    int currentFrame = 0;
    float time = 0f;
    Material material;

	// Use this for initialization
	void Start () {
        if (frames.Count > 0)
        {
            material = GetComponent<MeshRenderer>().material;
            material.SetTexture("_MainTex", frames[0].texture);
        }
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (frames.Count>0 && time>frames[currentFrame].duation)
        {
            time = 0;
            Debug.Log(currentFrame + " : " + (currentFrame + 1) % frames.Count);
            currentFrame = (currentFrame + 1) % frames.Count;
            material.SetTexture("_MainTex", frames[currentFrame].texture);
            Debug.Log(frames[currentFrame].texture.name);
        }
	}
}
