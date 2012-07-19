using UnityEngine;

/// <summary>
/// An action that, when triggered, cycles through a set of allowed RgbModes.
/// </summary>
public class ColorCycleAction : MonoBehaviour
{
	public RgbControl Target; // The object whose RgbMode will be cycled.

	public bool AllowR = true; // If true, RgbMode.R will be in the cycle.
	public bool AllowG = true; // If true, RgbMode.G will be in the cycle.
	public bool AllowB = true; // If true, RgbMode.B will be in the cycle.
	public bool AllowRG = false; // If true, RgbMode.Rg will be in the cycle.
	public bool AllowRB = false; // If true, RgbMode.Rb will be in the cycle.
	public bool AllowGB = false; // If true, RgbMode.Gb will be in the cycle.
	public bool AllowRGB = false; // If true, RgbMode.Rgb will be in the cycle.

	/// <summary>
	/// Called when the trigger has been activated.
	/// </summary>
	public void DoActivateTrigger()
	{
		if(Target == null)
			return;

		// Keep trying the next RgbMode until we find one that is allowed.
		RgbMode initMode = Target.RgbMode;
		do
		{
			Target.RgbMode = GetNextMode(Target.RgbMode);
		}
		while(!IsAllowed(Target.RgbMode) && Target.RgbMode != initMode);
	}

	/// <summary>
	/// Returns whether the specified RgbMode is currently allowed.
	/// </summary>
	private bool IsAllowed(RgbMode mode)
	{
		switch(mode)
		{
			case RgbMode.R: return AllowR;
			case RgbMode.G: return AllowG;
			case RgbMode.B: return AllowB;
			case RgbMode.Rg: return AllowRG;
			case RgbMode.Rb: return AllowRB;
			case RgbMode.Gb: return AllowGB;
			case RgbMode.Rgb: return AllowRGB;
			default: return false;
		}
	}

	/// <summary>
	/// Given the current RgbMode, returns the next RgbMode in the sequence, assuming all are allowed.
	/// </summary>
	private static RgbMode GetNextMode(RgbMode mode)
	{
		switch(mode)
		{
			case RgbMode.R: return RgbMode.G;
			case RgbMode.G: return RgbMode.B;
			case RgbMode.B: return RgbMode.Rg;
			case RgbMode.Rg: return RgbMode.Rb;
			case RgbMode.Rb: return RgbMode.Gb;
			case RgbMode.Gb: return RgbMode.Rgb;
			case RgbMode.Rgb: return RgbMode.R;
			default: return RgbMode.R;
		}
	}
}