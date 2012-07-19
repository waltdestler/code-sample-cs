using UnityEngine;

/// <summary>
/// When attached to an object, causes collisions between it and the specified Target to be ignored.
/// </summary>
[RequireComponent(typeof(Collider))]
public class IgnoreCollisionWith : MonoBehaviour
{
	public Collider Target;

	public void Start()
	{
		if(Target != null)
			Physics.IgnoreCollision(collider, Target);
	}
}