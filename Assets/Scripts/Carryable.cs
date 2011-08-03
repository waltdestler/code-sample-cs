using UnityEngine;

/// <summary>
/// Marks an object as able to be carried by the player
/// </summary>
public class Carryable : MonoBehaviour
{
	public bool IsCarryable = true;
	public float ControllerRadius = .8f;

	private bool _wasKinematic;
	private bool _wasColliderEnabled;
	private CharacterController _cc;

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

	public void MoveTo(Vector3 position, Quaternion rotation)
	{
		//transform.position = position;
		Vector3 posDiff = position - transform.position;
		_cc.Move(posDiff);
		transform.rotation = rotation;
	}
}