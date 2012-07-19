using UnityEngine;

/// <summary>
/// A point on an object used to check for line-of-sight to the player.
/// </summary>
public class WatchPoint : MonoBehaviour
{
	/// <summary>
	/// When selected in the editor, draws a line from the watch point to the player.
	/// </summary>
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