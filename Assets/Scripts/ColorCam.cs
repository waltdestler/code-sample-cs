using System;
using UnityEngine;

/// <summary>
/// Controls the camera for a single color channel. Renders the camera into a RenderTexture, which will
/// then be combined by CameraCombiner into the final output.
/// </summary>
[RequireComponent(typeof(Camera))]
public class ColorCam : MonoBehaviour
{
	[NonSerialized] public RenderTexture RenderTexture;

	public void LateUpdate()
	{
		// Recreate the RenderTexture if it doesn't exist or isn't the right size.
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