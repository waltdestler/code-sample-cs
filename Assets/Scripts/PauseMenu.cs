using UnityEngine;

[ExecuteInEditMode]
public class PauseMenu : MonoBehaviour
{
	public int Width = 300;
	public int Height = 300;
	public bool Paused;

	private string _levelText;

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Paused)
				UnPause();
			else
				Pause();
		}
	}

	public void OnGUI()
	{
		if(!Paused)
			return;

		Rect r = new Rect(Screen.width / 2 - Width / 2, Screen.height / 2 - Height / 2, Width, Height);
		GUI.Window(1, r, OnGuiWindow, "Paused");
	}

	private void OnGuiWindow(int id)
	{
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

		GUILayout.Label("Color Blind Hue Shift:");
		Settings.ColorBlindHueShift = GUILayout.HorizontalSlider(Settings.ColorBlindHueShift, 0, 360);

		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Resume"))
			UnPause();

		if(GUILayout.Button("Quit Game"))
			Application.Quit();
	}

	private void Pause()
	{
		Paused = true;
		Time.timeScale = 0;
		_levelText = Application.loadedLevel.ToString();
		Screen.showCursor = true;
		Screen.lockCursor = false;
	}

	private void UnPause()
	{
		Paused = false;
		Time.timeScale = 1;
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}
}