using System;
using UnityEngine;

/// <summary>
/// Renders one scene color into a render texture.
/// </summary>
[RequireComponent(typeof(Camera))]
public class ColorCam : MonoBehaviour
{
	[NonSerialized] public RenderTexture RenderTexture;

	public void LateUpdate()
	{
		if(RenderTexture == null || RenderTexture.width != Screen.width || RenderTexture.height != Screen.height)
		{
			if(RenderTexture != null)
				Destroy(RenderTexture);
			RenderTexture = new RenderTexture(Screen.width, Screen.height, 24);
			camera.targetTexture = RenderTexture;
		}
	}

	public void OnDestroy()
	{
		if(RenderTexture != null)
		{
			camera.targetTexture = null;
			Destroy(RenderTexture);
		}
	}
}