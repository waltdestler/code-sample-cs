using UnityEngine;

public static class Settings
{
	public static int CurrentLevel
	{
		get { return PlayerPrefs.GetInt("CurrentLevel", 1); }
		set
		{
			if(value != CurrentLevel)
				PlayerPrefs.SetInt("CurrentLevel", value);
		}
	}

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