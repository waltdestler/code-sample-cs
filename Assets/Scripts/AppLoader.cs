using UnityEngine;

public class AppLoader : MonoBehaviour
{
	public void Start()
	{
		Application.LoadLevel(Settings.CurrentLevel);
	}
}