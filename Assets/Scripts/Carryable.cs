using UnityEngine;

/// <summary>
/// When attached to an object, the object becomes "carryable" by the player.
/// </summary>
public class Carryable : MonoBehaviour
{
	public bool IsCarryable = true; // Set to false to disable carrying.
	public float ControllerRadius = .8f; // When being carried, this is the radius of the CharacterController attached to the object to handle collisions.

	private bool _wasKinematic;
	private bool _wasColliderEnabled;
	private CharacterController _cc;

	/// <summary>
	/// Called when the object is picked up by the player.
	/// </summary>
	public void OnCarry(Player player)
	{
		if(collider != null)
		{
			Physics.IgnoreCollision(player.collider, collider, true);
			_wasColliderEnabled = collider.enabled;
			collider.enabled = false;
		}
		if(rigidbody != null)
		{
			_wasKinematic = rigidbody.isKinematic;
			rigidbody.isKinematic = true;
		}

		_cc = gameObject.AddComponent<CharacterController>();
		_cc.radius = ControllerRadius;
	}

	/// <summary>
	/// Called when the object is put down by the player.
	/// </summary>
	public void OnDrop(Player player)
	{
		Destroy(_cc);
		if(rigidbody != null)
		{
			rigidbody.isKinematic = _wasKinematic;
			rigidbody.WakeUp();
		}
		if(collider != null)
		{
			collider.enabled = _wasColliderEnabled;
			Physics.IgnoreCollision(player.collider, collider, false);
		}
	}

	/// <summary>
	/// Called when the player moves to move the carried object to a specified position.
	/// </summary>
	public void MoveTo(Vector3 position, Quaternion rotation)
	{
		Vector3 posDiff = position - transform.position;
		_cc.Move(posDiff);
		transform.rotation = rotation;
	}
}