using System.Collections;
using UnityEngine;

/// <summary>
/// An action that, when triggered, moves the position of an object from one position to another over time.
/// </summary>
public class PositionAction : MonoBehaviour
{
	public Vector3 StartPos; // The object's starting position.
	public Vector3 EndPos; // The object's ending position.
	public float Duration = 1; // The amount of time it takes to get from StartPos to EndPos.
	public float Delay = 0; // The delay after triggering before movement starts.
	public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1); // The curve by which the object will be moved. By default it is linear.

	private bool _running; // Whether the movement is running, in which case it should not be interrupted.

	/// <summary>
	/// Called when this action has been triggered.
	/// </summary>
	public void DoActivateTrigger()
	{
		if(!_running)
			StartCoroutine(RunAction());
	}

	/// <summary>
	/// A coroutine that first waits for Delay, and then moves the object from StartPos to EndPos over time using Curve.
	/// </summary>
	private IEnumerator RunAction()
	{
		_running = true;
		yield return new WaitForSeconds(Delay);
		float startTime = Time.time;
		do
		{
			yield return 0;
			float t = Curve.Evaluate((Time.time - startTime) / Duration);
			transform.position = Vector3.Lerp(StartPos, EndPos, t);
		}
		while(Time.time - startTime <= Duration);
		_running = false;
	}
}