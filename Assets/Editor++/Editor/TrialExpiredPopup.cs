using UnityEditor;

/// <summary>
/// A popup window that displays the trial expired message.
/// </summary>
public class TrialExpiredPopup : EditorWindow
{
	#region Unity Methods

	public void OnGUI()
	{
		if(!DemoVerifier.AllowUse)
		{
			DemoVerifier.TrialExpiredGUI();
			return;
		}
	}

	#endregion
	#region Public Static Methods

	/// <summary>
	/// Shows the trial expired popup window.
	/// </summary>
	public static void ShowTrialExpiredPopup()
	{
		GetWindow<TrialExpiredPopup>();
	}

	#endregion
}