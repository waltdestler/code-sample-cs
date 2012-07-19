using UnityEngine;

/// <summary>
/// Emits a beam of light that changes the color of whatever object it hits.
/// </summary>
public class BeamEmitter : MonoBehaviour
{
	public float Range = 100; // How long the beam extends for.
	public RgbControl Beam; // The actual beam object with associated RgbMode.
	
	public void OnEnable()
	{
		Beam.gameObject.SetActiveRecursively(true);
	}
	
	public void OnDisable()
	{
		if(Beam != null)
			Beam.gameObject.SetActiveRecursively(false);
	}
	
	public void LateUpdate()
	{
		// Raycast to see what the beam hits.
		Ray ray = new Ray(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Range))
		{
			// Set the position of the beam object.
			Beam.transform.position = (transform.position + hit.point) / 2;
			Vector3 scale = Beam.transform.localScale;
			scale.y = hit.distance / 2;
			Beam.transform.localScale = scale;
			
			// Set mode override of the hit object.
			RgbControl rgbc = hit.collider.GetComponent<RgbControl>();
			if(rgbc != null)
			{
				if(rgbc.RgbModeOverride == null)
					rgbc.RgbModeOverride = Beam.EffectiveRgbMode;
				else
					rgbc.RgbModeOverride = rgbc.RgbModeOverride.Value | Beam.EffectiveRgbMode;
			}
		}
		else
		{
			// Set the position of the beam object.
			Vector3 point = transform.TransformPoint(new Vector3(0, 0, Range));
			Beam.transform.position = (transform.position + point) / 2;
			Vector3 scale = Beam.transform.localScale;
			scale.y = Range / 2;
			Beam.transform.localScale = scale;
		}
	}
}
