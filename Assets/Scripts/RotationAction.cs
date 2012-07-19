using System.Collections;
using UnityEngine;

/// <summary>
/// An action that, when triggered, increases or decreases the rotation of an object over time.
/// </summary>
public class RotationAction : MonoBehaviour
{
	public Vector3 RotationIncrease; // The amount of rotation to add (or subtract) over time.
	public float Duration = 1; // The amount of time it takes to complete the added rotation.
	public float Delay = 0; // The delay after triggering before movement starts.
	public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1); // The curve by which the object will be moved. By default it is linear.

	private bool _running; // Whether the rotation is running, in which case it should not be interrupted.

	/// <summary>
	/// Called when this action has been triggered.
	/// </summary>
	public void DoActivateTrigger()
	{
		if(!_running)
			StartCoroutine(RunAction());
	}

	/// <summary>
	/// A coroutine that first waits for Delay, and then adds RotationIncrease over time using Curve.
	/// </summary>
	private IEnumerator RunAction()
	{
		_running = true;
		yield return new WaitForSeconds(Delay);
		float startTime = Time.time;
		Vector3 startRot = transform.localEulerAngles;
		do
		{
			yield return 0;
			float t = Curve.Evaluate((Time.time - startTime) / Duration);
			transform.localEulerAngles = startRot + RotationIncrease * t;
		}
		while(Time.time - startTime <= Duration);
		_running = false;
	}
}