using UnityEngine;

public class BeamEmitter : MonoBehaviour
{
	public float Range = 100;
	public RgbControl Beam;
	public RgbControl RgbControl;
	
	public void OnEnable()
	{
		Beam.gameObject.SetActiveRecursively(true);
	}
	
	public void OnDisable()
	{
		Beam.gameObject.SetActiveRecursively(false);
	}
	
	public void LateUpdate()
	{
		Ray ray = new Ray(transform.position, transform.TransformDirection(new Vector3(0, 0, 1)));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Range))
		{
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
			Vector3 point = transform.TransformPoint(new Vector3(0, 0, Range));
			Beam.transform.position = (transform.position + point) / 2;
			Vector3 scale = Beam.transform.localScale;
			scale.y = Range / 2;
			Beam.transform.localScale = scale;
		}
	}
}
