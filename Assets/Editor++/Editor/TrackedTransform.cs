using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks a transform component, with each of the 9 variables tracked seperately.
/// </summary>
class TrackedTransform
{
	#region Private Fields

	private readonly float[] _values = new float[9];
	private readonly bool[] _changed = new bool[9];

	#endregion
	#region Properties

	public Transform Transform { get; private set; }

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new TrackedTransform that tracks the specified transform.
	/// </summary>
	public TrackedTransform(Transform transform)
	{
		Transform = transform;
		_values[0] = transform.localPosition.x;
		_values[1] = transform.localPosition.y;
		_values[2] = transform.localPosition.z;
		_values[3] = transform.localEulerAngles.x;
		_values[4] = transform.localEulerAngles.y;
		_values[5] = transform.localEulerAngles.z;
		_values[6] = transform.localScale.x;
		_values[7] = transform.localScale.y;
		_values[8] = transform.localScale.z;
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Updates the current values of the tracked transform.
	/// </summary>
	/// <returns>The transform variables that have changed.</returns>
	public IEnumerable<TransformVariable> Update()
	{
		if(Input.GetMouseButton(0))
			return new TransformVariable[0];
		
		List<TransformVariable> ret = new List<TransformVariable>();
		float[] newValues = new float[9];
		newValues[0] = Transform.localPosition.x;
		newValues[1] = Transform.localPosition.y;
		newValues[2] = Transform.localPosition.z;
		newValues[3] = Transform.localEulerAngles.x;
		newValues[4] = Transform.localEulerAngles.y;
		newValues[5] = Transform.localEulerAngles.z;
		newValues[6] = Transform.localScale.x;
		newValues[7] = Transform.localScale.y;
		newValues[8] = Transform.localScale.z;
		for(int i = 0; i < 9; i++)
		{
			if(newValues[i] != _values[i])
			{
				_values[i] = newValues[i];
				_changed[i] = true;
				ret.Add((TransformVariable)i);
			}
			else
			{
				_changed[i] = false;
			}
		}

		return ret;
	}

	/// <summary>
	/// Gets the current value of the specified transform variable.
	/// </summary>
	public float GetCurrentValue(TransformVariable v)
	{
		return _values[(int)v];
	}

	/// <summary>
	/// Sets the value of the specified transform variable.
	/// </summary>
	public void SetValue(TransformVariable v, float value)
	{
		_values[(int)v] = value;
		switch(v)
		{
			case TransformVariable.LocalXPos:
			case TransformVariable.LocalYPos:
			case TransformVariable.LocalZPos:
				Vector3 localPos = Transform.localPosition;
				switch(v)
				{
					case TransformVariable.LocalXPos:
						localPos.x = value;
						break;
					case TransformVariable.LocalYPos:
						localPos.y = value;
						break;
					case TransformVariable.LocalZPos:
						localPos.z = value;
						break;
				}
				Transform.localPosition = localPos;
				break;
			case TransformVariable.LocalXRot:
			case TransformVariable.LocalYRot:
			case TransformVariable.LocalZRot:
				Vector3 localRot = Transform.localEulerAngles;
				switch(v)
				{
					case TransformVariable.LocalXRot:
						localRot.x = value;
						break;
					case TransformVariable.LocalYRot:
						localRot.y = value;
						break;
					case TransformVariable.LocalZRot:
						localRot.z = value;
						break;
				}
				Transform.localEulerAngles = localRot;
				break;
			case TransformVariable.LocalXScale:
			case TransformVariable.LocalYScale:
			case TransformVariable.LocalZScale:
				Vector3 localScale = Transform.localScale;
				switch(v)
				{
					case TransformVariable.LocalXScale:
						localScale.x = value;
						break;
					case TransformVariable.LocalYScale:
						localScale.y = value;
						break;
					case TransformVariable.LocalZScale:
						localScale.z = value;
						break;
				}
				Transform.localScale = localScale;
				break;
		}
	}

	/// <summary>
	/// Gets whether the specified transform variable has changed.
	/// </summary>
	public bool GetChanged(TransformVariable v)
	{
		return _changed[(int)v];
	}

	#endregion
	#region Types

	public enum TransformVariable
	{
		LocalXPos,
		LocalYPos,
		LocalZPos,
		LocalXRot,
		LocalYRot,
		LocalZRot,
		LocalXScale,
		LocalYScale,
		LocalZScale,
	}

	#endregion
}