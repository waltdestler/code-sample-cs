using System.Collections;
using UnityEngine;

/// <summary>
/// An action that, when triggered, fades out the current level and loads the next level.
/// </summary>
public class NextLevelAction : MonoBehaviour
{
	public float Delay = 0; // The time before the fade-out begins.

	/// <summary>
	/// Called when this action has been triggered.
	/// </summary>
	public void DoActivateTrigger()
	{
		StartCoroutine(NextLevel());
	}

	/// <summary>
	/// A coroutine that waits for Delay, fades out the camera, and then loads the next level.
	/// </summary>
	private IEnumerator NextLevel()
	{
		yield return new WaitForSeconds(Delay);
		yield return Player.Current.CameraCombiner.StartFadeOut();
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}