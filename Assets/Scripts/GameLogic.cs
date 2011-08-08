using UnityEngine;

public class GameLogic : MonoBehaviour
{
	public void Start()
	{
		Settings.CurrentLevel = Application.loadedLevel;
	}
}