using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Current { get; private set; }

	public Transform Eyepoint;
	public Transform CarryPoint;
	public float TouchRange = 2;
	public CameraCombiner CameraCombiner;

	private Carryable _carriedObject;

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
					RaycastHit hit;
					if(!Physics.Raycast(ray, out hit, TouchRange))
						continue;
					if(hit.collider != c)
						continue;

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
							continue;
						}
						
						// Clickable?
						ClickTrigger ct = c.GetComponent<ClickTrigger>();
						if(ct != null)
						{
							closestCarryable = null;
							closestClickTrigger = ct;
							closestSqrDist = sqrDist;
							continue;
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