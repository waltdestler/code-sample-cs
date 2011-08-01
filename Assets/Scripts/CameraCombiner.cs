using System.Collections;
using UnityEngine;

/// <summary>
/// Combines the outputs of three cameras into one.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraCombiner : MonoBehaviour
{
	public ColorCam RedCamera;
	public ColorCam GreenCamera;
	public ColorCam BlueCamera;
	public Material FullscreenQuadMaterial;
	public float FadeInTime = 1;
	public float FadeOutTime = 1;

	private Material _mat;
	
	public void Awake()
	{
		_mat = new Material(FullscreenQuadMaterial);
	}

	public void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void OnPostRender()
	{
		_mat.SetTexture("_RedTex", RedCamera.RenderTexture);
		_mat.SetTexture("_GreenTex", GreenCamera.RenderTexture);
		_mat.SetTexture("_BlueTex", BlueCamera.RenderTexture);
		_mat.SetPass(0);

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

	public void OnDestroy()
	{
		Destroy(_mat);
	}

	public Coroutine StartFadeOut()
	{
		return StartCoroutine(FadeOut());
	}

	private IEnumerator FadeIn()
	{
		Color startColor = Color.black;
		Color endColor = _mat.GetColor("_PreColor");
		_mat.SetColor("_PreColor", startColor);
		yield return 0;
		float startTime = Time.time;
		do
		{
			yield return 0;
			Color c = Color.Lerp(startColor, endColor, (Time.time - startTime) / FadeInTime);
			_mat.SetColor("_PreColor", c);
		}
		while(Time.time - startTime <= FadeInTime);
	}

	private IEnumerator FadeOut()
	{
		Color startColor = _mat.GetColor("_PreColor");
		Color endColor = Color.black;
		_mat.SetColor("_PreColor", startColor);
		yield return 0;
		float startTime = Time.time;
		do
		{
			Color c = Color.Lerp(startColor, endColor, (Time.time - startTime) / FadeOutTime);
			_mat.SetColor("_PreColor", c);
			yield return 0;
		}
		while(Time.time - startTime <= FadeOutTime);
	}
}