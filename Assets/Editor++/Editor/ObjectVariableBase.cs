using System;

/// <summary>
/// Abstract base class for object variables.
/// </summary>
public abstract class ObjectVariableBase
{
	#region Properties

	public abstract Type VariableType { get; }
	public abstract string VariableName { get; }

	#endregion
	#region Public Methods

	public abstract object GetValue(object obj);
	public abstract void SetValue(object obj, object value);

	#endregion
}