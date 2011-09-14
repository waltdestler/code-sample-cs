using UnityEngine;

public class PositionResetAction : MonoBehaviour
{
	private Vector3 _pos;
	private Quaternion _rot;
	private Vector3 _vel;
	private Vector3 _angVel;

	public void Awake()
	{
		_pos = transform.position;
		_rot = transform.rotation;
		if(rigidbody != null)
		{
			_vel = rigidbody.velocity;
			_angVel = rigidbody.angularVelocity;
		}
	}

	public void DoActivateTrigger()
	{
		transform.position = _pos;
		transform.rotation = _rot;
		if(rigidbody != null)
		{
			rigidbody.velocity = _vel;
			rigidbody.angularVelocity = _angVel;
		}
	}
}