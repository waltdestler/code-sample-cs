using UnityEngine;

/// <summary>
/// Causes the transform of the attached object to always "look at" the player.
/// </summary>
public class LookAtPlayer : MonoBehaviour
{
	public bool LockXRot; // If set, the X rotation will not be modified.
	public bool LockYRot; // If set, the Y rotation will not be modified.
	public bool LockZRot; // If set, the Z rotation will not be modified.

	public void Update()
	{
		Vector3 oldAngles = transform.localEulerAngles;
		transform.LookAt(Player.Current.transform);
		Vector3 newAngles = transform.localEulerAngles;
		if(LockXRot)
			newAngles.x = oldAngles.x;
		if(LockYRot)
			newAngles.y = oldAngles.y;
		if(LockZRot)
			newAngles.z = oldAngles.z;
		transform.localEulerAngles = newAngles;
	}
}