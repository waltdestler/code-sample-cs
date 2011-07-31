using UnityEngine;

public class WatchPoint : MonoBehaviour
{
	public void OnDrawGizmosSelected()
	{
		Player player = Player.Current ?? (Player)FindObjectOfType(typeof(Player));
		if(player != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, player.transform.position);
		}
	}
}