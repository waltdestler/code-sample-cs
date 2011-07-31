using System;
using UnityEngine;

/// <summary>
/// A serializable object in the Resources folder that stores global constants.
/// </summary>
public class Constants : ScriptableObject
{
	#region Public Fields

	public Material RgbMaterial;
	public Material RMaterial;
	public Material GMaterial;
	public Material BMaterial;
	public Material RgMaterial;
	public Material RbMaterial;
	public Material GbMaterial;

	#endregion
	#region Private Static Fields

	private static Constants _global;

	#endregion
	#region Static Properties

	/// <summary>
	/// Gets the global Constants object.
	/// </summary>
	public static Constants Global
	{
		get
		{
			if(_global == null)
				_global = (Constants)Resources.Load("Constants", typeof(Constants));
			return _global;
		}
	}

	#endregion
	#region Public Methods

	public Material GetMaterial(RgbMode mode)
	{
		switch(mode)
		{
			case RgbMode.Rgb: return RgbMaterial;
			case RgbMode.R: return RMaterial;
			case RgbMode.G: return GMaterial;
			case RgbMode.B: return BMaterial;
			case RgbMode.Rg: return RgMaterial;
			case RgbMode.Rb: return RbMaterial;
			case RgbMode.Gb: return GbMaterial;
			default: return null;
		}
	}

	#endregion
}

[Flags] public enum RgbMode
{
	Rgb = 7,
	R = 1,
	G = 2,
	B = 4,
	Rg = 3,
	Rb = 5,
	Gb = 6,
}