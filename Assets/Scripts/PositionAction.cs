using System.Collections;
using UnityEngine;

public class PositionAction : MonoBehaviour
{
	public Vector3 StartPos;
	public Vector3 EndPos;
	public float Duration = 1;
	public float Delay = 0;
	public AnimationCurve Curve;

	public void DoActivateTrigger()
	{
		StartCoroutine(RunAction());
	}

	private IEnumerator RunAction()
	{
		yield return new WaitForSeconds(Delay);
		float startTime = Time.time;
		do
		{
			yield return 0;
			float t = Curve.Evaluate((Time.time - startTime) / Duration);
			transform.position = Vector3.Lerp(StartPos, EndPos, t);
		}
		while(Time.time - startTime <= Duration);
	}
}