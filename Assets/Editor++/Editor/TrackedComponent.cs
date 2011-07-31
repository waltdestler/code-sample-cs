using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Tracks the variables of a component.
/// </summary>
class TrackedComponent
{
	#region Private Fields

	private readonly Dictionary<string, TrackedVariable> _trackedVariables = new Dictionary<string, TrackedVariable>();

	#endregion
	#region Properties

	/// <summary>
	/// The component that is being tracked.
	/// </summary>
	public Component Component { get; private set; }

	/// <summary>
	/// Gets the tracked variable for the specified member name, or null if it is not being tracked.
	/// </summary>
	public TrackedVariable this[string name]
	{
		get
		{
			if(_trackedVariables.ContainsKey(name))
				return _trackedVariables[name];
			else
				return null;
		}
	}

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new TrackedComponent that tracks the variables of the specified component.
	/// </summary>
	public TrackedComponent(Component component)
	{
		if(component == null)
			throw new ArgumentNullException("component");

		Component = component;
		foreach(ObjectVariableBase ov in ObjectVariables.GetObjectVariables(component.GetType()))
			_trackedVariables[ov.VariableName] = new TrackedVariable(component, ov);
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Updates the current value of all variables tracked by this tracked component.
	/// </summary>
	/// <returns>The tracked variables that have changed.</returns>
	public IEnumerable<TrackedVariable> Update()
	{
		List<TrackedVariable> changes = new List<TrackedVariable>();
		foreach(TrackedVariable tv in _trackedVariables.Values)
		{
			if(tv.Update())
				changes.Add(tv);
		}
		return changes;
	}

	#endregion
}