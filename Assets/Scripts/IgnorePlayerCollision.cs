using UnityEngine;

/// <summary>
/// When attached to an object, causes it to ignore any collisions with the player.
/// </summary>
[RequireComponent(typeof(Collider))]
public class IgnorePlayerCollision : MonoBehaviour
{
	public void Start()
	{
		Physics.IgnoreCollision(collider, Player.Current.collider);
	}
}