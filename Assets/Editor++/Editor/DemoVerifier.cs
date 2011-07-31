//#define ENABLE_TRIAL

using UnityEditor;
using UnityEngine;
using System;

/// <summary>
/// Verifies whether the use of Editor++ is currently allowed.
/// </summary>
public static class DemoVerifier
{
	#region Constants

	private const string VERSION = "1.8";
	private const int TRIAL_DAYS = 14;

	#endregion
	#region Static Properties

	/// <summary>
	/// Returns whether Editor++ can currently be used.
	/// </summary>
	public static bool AllowUse
	{
#if ENABLE_TRIAL
		get
		{
			string storedVersion = EditorPrefs.GetString("Editor++.Version", VERSION);
			if(storedVersion != VERSION)
			{
				EditorPrefs.DeleteKey("Editor++.InstallDate");
				EditorPrefs.SetString("Editor++.Version", VERSION);
			}
			if(!EditorPrefs.HasKey("Editor++.InstallDate"))
				EditorPrefs.SetString("Editor++.InstallDate", DateTime.Now.ToString());
			DateTime installDate = DateTime.Parse(EditorPrefs.GetString("Editor++.InstallDate"));
			return DateTime.Now - installDate < TimeSpan.FromDays(TRIAL_DAYS);
		}
#else
		get { return true; }
#endif
	}

	#endregion
	#region Public Static Methods

	/// <summary>
	/// Shows the trial expired message.
	/// </summary>
	public static void TrialExpiredGUI()
	{
		GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
		labelStyle.wordWrap = true;

		Color old = GUI.contentColor;
		GUI.color = Color.red;
		GUILayout.Label("Your trial of Editor++ has expired. Please purchase the full version to continue use.", labelStyle);
		GUI.color = Color.green;
		if(GUILayout.Button("Purchase"))
			Application.OpenURL("http://u3d.as/content/walt-destler/editor-/1t1");
		GUI.color = old;
	}

	#endregion
}