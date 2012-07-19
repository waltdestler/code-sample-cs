using System;

/// <summary>
/// Enumerates the various options to which an object's "RgbMode" can be set.
/// An object's "RgbMode" defines its color, its layer, and how it interacts with the visibility of other objects.
/// </summary>
[Flags] public enum RgbMode
{
	/// <summary>
	/// The object exists only in the red color channel.
	/// </summary>
	R = 1,

	/// <summary>
	/// The object exists only in the green color channel.
	/// </summary>
	G = 2,

	/// <summary>
	/// The object exists only in the blue color channel.
	/// </summary>
	B = 4,

	/// <summary>
	/// The object exists in both the red and green color channels.
	/// </summary>
	Rg = 3,

	/// <summary>
	/// The object exists in both the red and blue color channels.
	/// </summary>
	Rb = 5,
	
	/// <summary>
	/// The object exists in both the green and blue color channels.
	/// </summary>
	Gb = 6,
	
	/// <summary>
	/// The object exists in all three red, green, and blue color channels.
	/// This is a special-case mode, in that objects using this mode do not follow the normal line-of-sight
	/// rules. That is, such objects do not block LOS to other objects and cannot themselves be blocked.
	/// </summary>
	Rgb = 7,
}