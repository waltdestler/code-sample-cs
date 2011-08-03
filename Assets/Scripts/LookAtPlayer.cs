using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	public bool LockXRot;
	public bool LockYRot;
	public bool LockZRot;

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