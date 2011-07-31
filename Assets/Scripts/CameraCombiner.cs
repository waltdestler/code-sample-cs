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

	private Material _mat;
	
	public void Awake()
	{
		_mat = new Material(FullscreenQuadMaterial);
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
}