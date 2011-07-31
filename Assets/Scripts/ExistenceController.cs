using UnityEngine;

[RequireComponent(typeof(RgbControl))]
public class ExistenceController : MonoBehaviour
{
	public Transform[] WatchPoints;
	public bool Permanent;

	private RgbControl _rgb;
	private bool _exists = true;
	private Collider _collider;

	public void Awake()
	{
		_rgb = GetComponent<RgbControl>();
		_collider = GetComponent<Collider>();
	}

	public void Update()
	{
		// Don't update if not close enough to player.
		Vector3 playerPos = Player.Current.Eyepoint.position;
		float sqrPlayerDist = (transform.position - playerPos).sqrMagnitude;
		float sqrFogDist = RenderSettings.fogEndDistance * RenderSettings.fogEndDistance;
		if(sqrPlayerDist > sqrFogDist)
			return;

		// If every single watch point is blocked, we don't exist.
		bool exists = false;
		foreach(Transform t in WatchPoints)
		{
			if(!IsLosBlocked(t.position, playerPos))
			{
				exists = true;
				break;
			}
		}

		if(Permanent)
		{
			// Do we no longer exist?
			if(!exists)
			{
				_exists = false;
				Destroy(gameObject);
				print("Destroyed " + name + " (LOS check).");
			}
		}
		else
		{
			// Switch from existing to non-existing and vice-versa?
			if(_exists && !exists)
			{
				_exists = false;
				//if(renderer != null)
				//	renderer.enabled = false;
				if(_collider != null)
					_collider.enabled = false;
				if(rigidbody != null)
					rigidbody.isKinematic = true;
				print("Hid " + name + " (LOS check).");
			}
			else if(!_exists && exists)
			{
				_exists = true;
				//if(renderer != null)
				//	renderer.enabled = true;
				if(_collider != null)
					_collider.enabled = true;
				if(rigidbody != null)
					rigidbody.isKinematic = false;
				print("Showed " + name + " (LOS check).");
			}
		}
	}

	private bool IsLosBlocked(Vector3 start, Vector3 end)
	{
		Vector3 posDiff = end - start;
		Ray ray = new Ray(start, posDiff);
		float dist = posDiff.magnitude;
		RgbMode mode = _rgb.RgbMode;

		// Need to check against R, G, and B independently...
		
		// Red?
		bool redBlocked;
		if((mode & RgbMode.R) != 0)
			redBlocked = Raycast(ray, dist, Layers.RCollisionMask);
		else
			redBlocked = true;

		// Green?
		bool greenBlocked;
		if((mode & RgbMode.G) != 0)
			greenBlocked = Raycast(ray, dist, Layers.GCollisionMask);
		else
			greenBlocked = true;

		// Blue?
		bool blueBlocked;
		if((mode & RgbMode.B) != 0)
			blueBlocked = Raycast(ray, dist, Layers.BCollisionMask);
		else
			blueBlocked = true;

		return redBlocked && greenBlocked && blueBlocked;
	}

	/// <summary>
	/// Performs a raycast while ignoring the current object.
	/// </summary>
	private bool Raycast(Ray ray, float dist, int layerMask)
	{
		RaycastHit[] hits = Physics.RaycastAll(ray, dist, layerMask);
		return hits.Length > 0 && (hits.Length > 1 || hits[0].transform != transform);
	}
}