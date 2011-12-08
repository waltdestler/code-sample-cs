using UnityEngine;

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