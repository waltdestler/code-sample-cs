using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RgbControl))]
public class ExistenceController : MonoBehaviour
{
	public Transform[] WatchPoints;
	public bool Permanent;
	public bool CoincisionSensitive;
	public bool AlterMass;

	private RgbControl _rgb;
	private bool _exists = true;
	private bool _originalIsKinematic;
	private float _originalMass;
	private readonly HashSet<Collider> _inTrigger = new HashSet<Collider>();

	public void Awake()
	{
		_rgb = GetComponent<RgbControl>();
		if(rigidbody != null)
		{
			_originalIsKinematic = rigidbody.isKinematic;
			_originalMass = rigidbody.mass;
		}
	}

	public void Update()
	{
		// Don't dissappear if not in rgb mode.
		if(_rgb.RgbMode == RgbMode.Rgb)
			return;

		// Don't dissappear if not close enough to player.
		Vector3 playerPos = Player.Current.Eyepoint.position;
		float sqrPlayerDist = (transform.position - playerPos).sqrMagnitude;
		float sqrFogDist = RenderSettings.fogEndDistance * RenderSettings.fogEndDistance;
		if(sqrPlayerDist > sqrFogDist)
			return;

		// If every single watch point is blocked, we don't exist.
		bool exists = false;
		// Don't re-exist if currently colliding with another object.
		if(_inTrigger.Count == 0)
		{
			foreach(Transform t in WatchPoints)
			{
				if(!IsLosBlocked(t.position, playerPos))
				{
					exists = true;
					break;
				}
			}
			
			if(particleEmitter != null)
				particleEmitter.emit = false;
		}
		else
		{
			if(particleEmitter != null)
				particleEmitter.emit = true;
		}

		if(Permanent)
		{
			// Do we no longer exist?
			if(!exists)
			{
				_exists = false;
				Destroy(gameObject);
				//print("Destroyed " + name + " (LOS check).");
			}
		}
		else
		{
			// Switch from existing to non-existing and vice-versa?
			if(_exists && !exists)
			{
				_exists = false;
				if(renderer != null)
					renderer.enabled = false;
				if(collider != null)
					collider.isTrigger = true;
				if(rigidbody != null)
				{
					if(!_originalIsKinematic && !AlterMass)
						rigidbody.isKinematic = true;
					if(AlterMass)
						rigidbody.mass = .0001f;
				}
				BeamEmitter be = GetComponentInChildren<BeamEmitter>();
				if(be != null)
					be.enabled = false;
				//print("Hid " + name + " (LOS check).");
			}
			else if(!_exists && exists)
			{
				_exists = true;
				// Don't re-exist if currently colliding with another object.
				//if(_inTrigger.Count == 0)
				{
					if(renderer != null)
						renderer.enabled = true;
					if(collider != null)
						collider.isTrigger = false;
					if(rigidbody != null)
					{
						if(!_originalIsKinematic && !AlterMass)
						{
							rigidbody.isKinematic = false;
							rigidbody.WakeUp();
						}
						if(AlterMass)
							rigidbody.mass = _originalMass;
					}
					BeamEmitter be = GetComponentInChildren<BeamEmitter>();
					if(be != null)
						be.enabled = true;
					//print("Showed " + name + " (LOS check).");
				}
			}
		}
	}
	
	public void FixedUpdate()
	{
		_inTrigger.Clear();
	}
	
	public void OnTriggerStay(Collider c)
	{
		if(collider.isTrigger && CoincisionSensitive)
		{
			_inTrigger.Add(c);
			//print("OnTriggerEnter " + c.name + " in " + name);
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
		List<RaycastHit> hits = new List<RaycastHit>(Physics.RaycastAll(ray, dist, layerMask).Where(rh => !rh.collider.isTrigger));
		return hits.Count > 0 && (hits.Count > 1 || hits[0].transform != transform);
	}
}