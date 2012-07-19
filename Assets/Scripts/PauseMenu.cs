using UnityEngine;

/// <summary>
/// The menu GUI shown when the game is paused.
/// </summary>
[ExecuteInEditMode]
public class PauseMenu : MonoBehaviour
{
	public int Width = 300;
	public int Height = 300;

	private bool _paused;
	private string _levelText;

	public void Update()
	{
		// Escape key pauses or unpauses the game.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(_paused)
				UnPause();
			else
				Pause();
		}
	}

	public void OnGUI()
	{
		// Don't show menu when paused.
		if(!_paused)
			return;

		Rect r = new Rect(Screen.width / 2 - Width / 2, Screen.height / 2 - Height / 2, Width, Height);
		GUI.Window(1, r, OnGuiWindow, "Paused");
	}

	/// <summary>
	/// Called by GUI.Window to draw/handle the GUI inside the pause menu window.
	/// </summary>
	private void OnGuiWindow(int id)
	{
		// Show text box to set current level.
		GUILayout.BeginHorizontal();
		GUILayout.Label("Current Level:");
		_levelText = GUILayout.TextField(_levelText);
		if(GUILayout.Button("GO"))
		{
			int levelNum;
			if(int.TryParse(_levelText, out levelNum))
				Application.LoadLevel(levelNum);
			else
				Application.LoadLevel(_levelText);
			UnPause();
		}
		GUILayout.EndHorizontal();

		// Show hue-shift slider.
		GUILayout.Label("Color Blind Hue Shift:");
		Settings.ColorBlindHueShift = GUILayout.HorizontalSlider(Settings.ColorBlindHueShift, 0, 360);

		// Show graphics quality settings.
		GUILayout.Label("Graphics Quality:");
		GUILayout.BeginHorizontal();
		for(int i = 0; i < QualitySettings.names.Length; i++)
		{
			string qualityName = QualitySettings.names[i];
			if(GUILayout.Toggle(QualitySettings.GetQualityLevel() == i, qualityName))
				QualitySettings.SetQualityLevel(i);
		}
		GUILayout.EndHorizontal();

		// Show resume button.
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Resume"))
			UnPause();

		// Show quit button.
		if(GUILayout.Button("Quit Game"))
			Application.Quit();
	}

	/// <summary>
	/// Called when the player has paused the game.
	/// </summary>
	private void Pause()
	{
		_paused = true;
		Time.timeScale = 0;
		_levelText = Application.loadedLevel.ToString();
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}

	/// <summary>
	/// Called when the player has un-paused the game.
	/// </summary>
	private void UnPause()
	{
		_paused = false;
		Time.timeScale = 1;
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}
}