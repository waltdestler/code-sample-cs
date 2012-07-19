using System;
using UnityEngine;

/// <summary>
/// Controls the color (material) and layer of the associated game object depending on a specified "RgbMode".
/// </summary>
[ExecuteInEditMode, RequireComponent(typeof(Renderer))]
public class RgbControl : MonoBehaviour
{
	/// <summary>
	/// The base RgbMode for the object. See the comments for the RgbMode enum for more details.
	/// </summary>
	public RgbMode RgbMode = RgbMode.Rgb;
	
	/// <summary>
	/// If not-null, overrides the RgbMode field. Will be cleared every frame during Update.
	/// </summary>
	[NonSerialized] public RgbMode? RgbModeOverride;

	private RgbMode? _curMode; // Used to track the last rgb mode so that the material and layer are only set when the mode is changed.
	
	/// <summary>
	/// Returns the value of RgbModeOverride, or the value of RgbMode if RgbModeOverride is null.
	/// </summary>
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

		// Has the mode changed?
		if(mode != _curMode)
		{
			_curMode = mode;

			// Set material and layer.
			renderer.sharedMaterial = Constants.Global.GetMaterial(mode);
			gameObject.layer = Layers.FromRgbMode(mode);
			ParticleRenderer pr = GetComponent<ParticleRenderer>();
			if(pr != null)
				pr.sharedMaterial = renderer.sharedMaterial;
		}
		
		RgbModeOverride = null; // The override is cleared every frame and must be reset.
	}
}