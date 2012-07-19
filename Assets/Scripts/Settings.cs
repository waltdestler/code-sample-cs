using UnityEngine;

/// <summary>
/// Exposes various game settings that are stored between play sessions using PlayerPrefs.
/// </summary>
public static class Settings
{
	/// <summary>
	/// The index of the currently-loaded or last-played level.
	/// </summary>
	public static int CurrentLevel
	{
		get { return PlayerPrefs.GetInt("CurrentLevel", 1); }
		set
		{
			if(value != CurrentLevel)
				PlayerPrefs.SetInt("CurrentLevel", value);
		}
	}

	/// <summary>
	/// The degrees by which the entire color spectrum should be shifted.
	/// This may be a useful setting to be modified by color-blind players.
	/// </summary>
	public static float ColorBlindHueShift
	{
		get { return PlayerPrefs.GetFloat("ColorBlindHueShift", 0); }
		set
		{
			if(value != ColorBlindHueShift)
				PlayerPrefs.SetFloat("ColorBlindHueShift", value);
		}
	}
}