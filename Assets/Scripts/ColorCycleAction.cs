using UnityEngine;

public class ColorCycleAction : MonoBehaviour
{
	public RgbControl Target;
	public bool AllowR = true;
	public bool AllowG = true;
	public bool AllowB = true;
	public bool AllowRG = false;
	public bool AllowRB = false;
	public bool AllowGB = false;
	public bool AllowRGB = false;

	public void DoActivateTrigger()
	{
		if(Target == null)
			return;

		RgbMode initMode = Target.RgbMode;
		do
		{
			Target.RgbMode = GetNextMode(Target.RgbMode);
		}
		while(!IsAllowed(Target.RgbMode) && Target.RgbMode != initMode);
	}

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

	private RgbMode GetNextMode(RgbMode mode)
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