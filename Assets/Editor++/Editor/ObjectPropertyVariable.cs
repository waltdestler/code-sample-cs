using System;
using System.Reflection;

/// <summary>
/// Allows access to a reflected object property.
/// </summary>
public class ObjectPropertyVariable : ObjectVariableBase
{
	#region Private Fields

	private readonly PropertyInfo _propInfo;

	#endregion
	#region Properties


	public override Type VariableType
	{
		get { return _propInfo.PropertyType; }
	}

	public override string VariableName
	{
		get { return _propInfo.Name; }
	}

	#endregion
	#region Constructors

	public ObjectPropertyVariable(PropertyInfo propInfo)
	{
		if(propInfo == null)
			throw new ArgumentNullException("propInfo");

		_propInfo = propInfo;
	}

	#endregion
	#region Public Methods

	public override object GetValue(object obj)
	{
		return _propInfo.GetValue(obj, null);
	}

	public override void SetValue(object obj, object value)
	{
		_propInfo.SetValue(obj, value, null);
	}

	#endregion
}