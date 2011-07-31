using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class RgbControl : MonoBehaviour
{
	public RgbMode RgbMode = RgbMode.Rgb;

	private RgbMode? _curMode;

	public void Awake()
	{
		LateUpdate();
	}

	public void LateUpdate()
	{
		if(RgbMode != _curMode)
		{
			_curMode = RgbMode;
			gameObject.layer = Layers.FromRgbMode(RgbMode);
			renderer.sharedMaterial = Constants.Global.GetMaterial(RgbMode);
		}
	}
}