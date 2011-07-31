using UnityEngine;

public class NextLevelAction: MonoBehaviour
{
	public void DoActivateTrigger()
	{
		Invoke("NextLevel", 2);
	}

	private void NextLevel()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}