using UnityEngine;

/// <summary>
/// Should be placed in the initially-loaded scene to load the appropriate level.
/// </summary>
public class AppLoader : MonoBehaviour
{
	public void Start()
	{
		// Load the last-played level.
		Application.LoadLevel(Settings.CurrentLevel);
	}
}