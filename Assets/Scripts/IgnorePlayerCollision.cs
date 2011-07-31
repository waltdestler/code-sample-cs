using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IgnorePlayerCollision : MonoBehaviour
{
	public void Start()
	{
		Physics.IgnoreCollision(collider, Player.Current.collider);
	}
}