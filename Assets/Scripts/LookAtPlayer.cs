using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	public void Update()
	{
		transform.LookAt(Player.Current.transform);
	}
}