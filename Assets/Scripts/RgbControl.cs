using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class RgbControl : MonoBehaviour
{
	public RgbMode RgbMode = RgbMode.Rgb;
	
	[NonSerialized] public RgbMode? RgbModeOverride;

	private RgbMode? _curMode;
	
	public RgbMode EffectiveRgbMode
	{
		get
		{
			return RgbModeOverride ?? RgbMode;
		}
	}

	public void Awake()
	{
		Update();
	}

	public void Update()
	{
		RgbMode mode = EffectiveRgbMode;
		if(mode != _curMode)
		{
			_curMode = mode;
			renderer.sharedMaterial = Constants.Global.GetMaterial(mode);
			renderer.gameObject.layer = Layers.FromRgbMode(mode);
		}
		
		RgbModeOverride = null;
	}
}