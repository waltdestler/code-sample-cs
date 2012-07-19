using UnityEngine;

/// <summary>
/// Contains high-level gameplay logic.
/// </summary>
public class GameLogic : MonoBehaviour
{
	public void Start()
	{
		// Record the current level into the settings.
		Settings.CurrentLevel = Application.loadedLevel;
	}
}