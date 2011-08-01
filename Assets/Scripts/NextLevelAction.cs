using System.Collections;
using UnityEngine;

public class NextLevelAction: MonoBehaviour
{
	public float Delay = 0;

	public void DoActivateTrigger()
	{
		StartCoroutine(NextLevel());
	}

	private IEnumerator NextLevel()
	{
		yield return new WaitForSeconds(Delay);
		yield return Player.Current.CameraCombiner.StartFadeOut();
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}