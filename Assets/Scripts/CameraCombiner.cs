using System.Collections;
using UnityEngine;

/// <summary>
/// Combines the outputs of three red, green, and blue channel cameras into one render.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraCombiner : MonoBehaviour
{
	public ColorCam RedCamera;
	public ColorCam GreenCamera;
	public ColorCam BlueCamera;
	public Material RedCombiner;
	public Material GreenCombiner;
	public Material BlueCombiner;
	public float FadeInTime = 1;
	public float FadeOutTime = 1;

	private Material _redMat;
	private Material _greenMat;
	private Material _blueMat;
	
	public void Awake()
	{
		_redMat = new Material(RedCombiner);
		_greenMat = new Material(GreenCombiner);
		_blueMat = new Material(BlueCombiner);
	}

	public void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void OnPostRender()
	{
		_redMat.SetTexture("_RedTex", RedCamera.RenderTexture);
		_redMat.SetPass(0);
		DrawFullscreenQuad();

		_greenMat.SetTexture("_GreenTex", GreenCamera.RenderTexture);
		_greenMat.SetPass(0);
		DrawFullscreenQuad();

		_blueMat.SetTexture("_BlueTex", BlueCamera.RenderTexture);
		_blueMat.SetPass(0);
		DrawFullscreenQuad();
	}

	public void OnDestroy()
	{
		Destroy(_redMat);
		Destroy(_greenMat);
		Destroy(_blueMat);
	}

	public Coroutine StartFadeOut()
	{
		return StartCoroutine(FadeOut());
	}

	private static void DrawFullscreenQuad()
	{
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.LoadIdentity();
		GL.Begin(GL.QUADS);
		GL.TexCoord2(0, 0);
		GL.Vertex3(0, 0, 0);
		GL.TexCoord2(0, 1);
		GL.Vertex3(0, 1, 0);
		GL.TexCoord2(1, 1);
		GL.Vertex3(1, 1, 0);
		GL.TexCoord2(1, 0);
		GL.Vertex3(1, 0, 0);
		GL.End();
		GL.PopMatrix();
	}

	/// <summary>
	/// A coroutine that fades from black into the game.
	/// </summary>
	private IEnumerator FadeIn()
	{
		Color startColor = Color.black;
		Color endColor = new Color(
			_redMat.GetFloat("_PreFactor"),
			_greenMat.GetFloat("_PreFactor"),
			_blueMat.GetFloat("_PreFactor"));
		_redMat.SetFloat("_PreFactor", startColor.r);
		_greenMat.SetFloat("_PreFactor", startColor.g);
		_blueMat.SetFloat("_PreFactor", startColor.b);
		yield return 0;
		float startTime = Time.time;
		do
		{
			yield return 0;
			Color c = Color.Lerp(startColor, endColor, (Time.time - startTime) / FadeInTime);
			_redMat.SetFloat("_PreFactor", c.r);
			_greenMat.SetFloat("_PreFactor", c.g);
			_blueMat.SetFloat("_PreFactor", c.b);
		}
		while(Time.time - startTime <= FadeInTime);
	}

	/// <summary>
	/// A coroutine that fades from the game out to black.
	/// </summary>
	private IEnumerator FadeOut()
	{
		Color startColor = new Color(
			_redMat.GetFloat("_PreFactor"),
			_greenMat.GetFloat("_PreFactor"),
			_blueMat.GetFloat("_PreFactor"));
		Color endColor = Color.black;
		_redMat.SetFloat("_PreFactor", startColor.r);
		_greenMat.SetFloat("_PreFactor", startColor.g);
		_blueMat.SetFloat("_PreFactor", startColor.b);
		yield return 0;
		float startTime = Time.time;
		do
		{
			Color c = Color.Lerp(startColor, endColor, (Time.time - startTime) / FadeOutTime);
			_redMat.SetFloat("_PreFactor", c.r);
			_greenMat.SetFloat("_PreFactor", c.g);
			_blueMat.SetFloat("_PreFactor", c.b);
			yield return 0;
		}
		while(Time.time - startTime <= FadeOutTime);
	}
}