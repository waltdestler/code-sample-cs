using UnityEngine;

/// <summary>
/// A simple action that, when triggered, resets the position of an object to its original position.
/// </summary>
public class PositionResetAction : MonoBehaviour
{
	private Vector3 _pos;
	private Quaternion _rot;
	private Vector3 _vel;
	private Vector3 _angVel;

	public void Awake()
	{
		// Record original transform and velocities.
		_pos = transform.position;
		_rot = transform.rotation;
		if(rigidbody != null)
		{
			_vel = rigidbody.velocity;
			_angVel = rigidbody.angularVelocity;
		}
	}

	/// <summary>
	/// Called when the trigger has been activated.
	/// </summary>
	public void DoActivateTrigger()
	{
		// Restore original transform and velocities.
		transform.position = _pos;
		transform.rotation = _rot;
		if(rigidbody != null)
		{
			rigidbody.velocity = _vel;
			rigidbody.angularVelocity = _angVel;
		}
	}
}