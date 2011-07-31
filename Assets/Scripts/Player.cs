using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Current { get; private set; }

	public Transform Eyepoint;

	public void OnEnable()
	{
		Screen.showCursor = false;
		Screen.lockCursor = true;
		if(Current == null)
			Current = this;
	}

	public void OnDisable()
	{
		Screen.showCursor = true;
		Screen.lockCursor = false;
		if(Current == this)
			Current = null;
	}
}