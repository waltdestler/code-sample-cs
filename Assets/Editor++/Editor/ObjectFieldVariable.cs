using System;
using System.Reflection;

/// <summary>
/// Allows access to a reflected object field.
/// </summary>
public class ObjectFieldVariable : ObjectVariableBase
{
	#region Private Fields

	private readonly FieldInfo _fieldInfo;

	#endregion
	#region Properties

	public override Type VariableType
	{
		get { return _fieldInfo.FieldType; }
	}

	public override string VariableName
	{
		get { return _fieldInfo.Name; }
	}

	#endregion
	#region Constructors

	public ObjectFieldVariable(FieldInfo fieldInfo)
	{
		if(fieldInfo == null)
			throw new ArgumentNullException("fieldInfo");

		_fieldInfo = fieldInfo;
	}

	#endregion
	#region Public Methods

	public override object GetValue(object obj)
	{
		return _fieldInfo.GetValue(obj);
	}

	public override void SetValue(object obj, object value)
	{
		_fieldInfo.SetValue(obj, value);
	}

	#endregion
}