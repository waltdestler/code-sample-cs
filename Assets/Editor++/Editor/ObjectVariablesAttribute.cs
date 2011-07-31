using System;

/// <summary>
/// Should be applied to static methods that return enumerations of object variables for a given type.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ObjectVariablesAttribute : Attribute
{
	public Type ObjectType { get; private set; }

	public ObjectVariablesAttribute(Type type)
	{
		ObjectType = type;
	}
}