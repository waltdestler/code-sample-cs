using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains object for the player-controlled game object.
/// </summary>
public class Player : MonoBehaviour
{
	/// <summary>
	/// The current Player object in the scene.
	/// </summary>
	public static Player Current { get; private set; }

	public Transform Eyepoint; // Line-of-sight checks will be made to this point.
	public Transform CarryPoint; // Carried objects will be set to this point.
	public float TouchRange = 2; // Maximum distance from which an object can be picked up or clicked.
	public CameraCombiner CameraCombiner; // The object that combines the outputs of the separate red, green, and blue cameras.

	private Carryable _carriedObject; // The object currently being carried, if any.

	public void OnEnable()
	{
		Screen.showCursor = false;
		Screen.lockCursor = true;
		if(Current == null)
			Current = this;
	}

	public void OnDisable()
	{
		Screen.showCursor = true;
		Screen.lockCursor = false;
		if(Current == this)
			Current = null;
	}

	public void Update()
	{
		// Don't update if paused.
		if(Time.timeScale == 0)
			return;

		// Pick up / put down object?
		if(Input.GetButtonDown("Fire1"))
		{
			// If carrying an object, put it down.
			if(_carriedObject != null)
			{
				_carriedObject.OnDrop(this);
				_carriedObject = null;
			}
			else
			{
				// Find closest interactable object within range.
				Collider[] colliders = Physics.OverlapSphere(transform.position, TouchRange);
				Carryable closestCarryable = null;
				ClickTrigger closestClickTrigger = null;
				float closestSqrDist = float.MaxValue;
				foreach(Collider c in colliders)
				{
					// Throw out if not on screen.
					Vector3 screenPoint = Camera.main.WorldToScreenPoint(c.transform.position);
					if(screenPoint.x < 0 || screenPoint.x >= Screen.width || screenPoint.y < 0 || screenPoint.y >= Screen.height)
						continue;

					// Raycast to find edge of object.
					Ray ray = new Ray(transform.position, c.transform.position - transform.position);
					float range = Mathf.Min(TouchRange, (c.transform.position - transform.position).magnitude);
					List<RaycastHit> hits = new List<RaycastHit>(Physics.RaycastAll(ray, range).Where(rh => !rh.collider.isTrigger));
					if(hits.Count != 1 || hits[0].collider != c)
						continue;
					RaycastHit hit = hits[0];

					// In range and current closest?
					float sqrDist = (hit.point - transform.position).sqrMagnitude;
					if(sqrDist < closestSqrDist && sqrDist < TouchRange * TouchRange)
					{
						// Carryable?
						Carryable carryable = c.GetComponent<Carryable>();
						if(carryable != null && carryable.IsCarryable)
						{
							closestCarryable = carryable;
							closestClickTrigger = null;
							closestSqrDist = sqrDist;
						}
						
						// Clickable?
						ClickTrigger ct = c.GetComponent<ClickTrigger>();
						if(ct != null)
						{
							closestCarryable = null;
							closestClickTrigger = ct;
							closestSqrDist = sqrDist;
						}
					}
				}

				// Found a carryable?
				if(closestCarryable != null)
				{
					_carriedObject = closestCarryable;
					_carriedObject.OnCarry(this);
				}

				// Found a clickable?
				if(closestClickTrigger != null)
				{
					closestClickTrigger.Click();
				}
			}
		}
	}

	public void LateUpdate()
	{
		// Don't update if paused.
		if(Time.timeScale == 0)
			return;

		// Move carried object to the carry point.
		if(_carriedObject != null)
			_carriedObject.MoveTo(CarryPoint.position, CarryPoint.rotation);
	}
}