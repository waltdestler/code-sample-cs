using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Current { get; private set; }

	public Transform Eyepoint;
	public Transform CarryPoint;
	public float PickUpRange = 2;
	public float RadiusWhenCarrying = 1;

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
		// Pick up / put down object?
		if(Input.GetButtonDown("Fire1"))
		{
			// Pick up if not carrying.
			if(_carriedObject == null)
			{
				// Raycast to get object.
				Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, PickUpRange))
				{
					Carryable carryable = hit.transform.GetComponent<Carryable>();
					if(carryable != null && carryable.IsCarryable)
					{
						_carriedObject = carryable;
						_carriedObject.OnCarry(this);
					}
				}
			}
			else // Put down.
			{
				_carriedObject.OnDrop(this);
				_carriedObject = null;
			}
		}
	}

	public void LateUpdate()
	{
		// Move carried object to the carry point.
		if(_carriedObject != null)
			_carriedObject.MoveTo(CarryPoint.position, CarryPoint.rotation);
	}
}