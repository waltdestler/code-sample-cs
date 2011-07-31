using System;

/// <summary>
/// Tracks the current value of a single variable of an object.
/// </summary>
class TrackedVariable
{
	#region Private Fields

	private readonly ObjectVariableBase _ov;

	#endregion
	#region Properties

	/// <summary>
	/// Gets the object of the tracked variable.
	/// </summary>
	public object Object { get; private set; }

	/// <summary>
	/// Gets the current value of the tracked variable.
	/// </summary>
	public object CurrentValue { get; private set; }

	/// <summary>
	/// Gets whether the value of this variable changed between the two previous calls to Update().
	/// </summary>
	public bool Changed { get; private set; }

	/// <summary>
	/// Gets the name of the tracked variable.
	/// </summary>
	public string Name { get { return _ov.VariableName; } }

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new TrackedVariable that tracks the value of the specified object variable.
	/// </summary>
	public TrackedVariable(object obj, ObjectVariableBase ov)
	{
		if(obj == null)
			throw new ArgumentNullException("obj");
		if(ov == null)
			throw new ArgumentNullException("ov");

		Object = obj;
		_ov = ov;
		CurrentValue = ov.GetValue(obj);
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Updates the current value of the tracked variable.
	/// </summary>
	/// <returns>True if the value has changed.</returns>
	public bool Update()
	{
		object newValue = _ov.GetValue(Object);
		if(!CheckEquals(newValue, CurrentValue))
		{
			CurrentValue = newValue;
			Changed = true;
			return true;
		}
		else
		{
			Changed = false;
			return false;
		}
	}

	/// <summary>
	/// Sets the current value of the tracked variable to the specified value.
	/// </summary>
	public void SetValue(object value)
	{
		_ov.SetValue(Object, value);
		CurrentValue = value;
	}

	#endregion
	#region Private Methods
	
	/// <summary>
	/// Returns whether the specified objects are equal.
	/// </summary>
	private bool CheckEquals(object obj1, object obj2)
	{
		if(obj1 == null)
		{
			return obj2 == null;
		}
		else if(obj2 == null)
		{
			return false;
		}
		if(obj1 is Array && obj2 is Array)
		{
			object[] arr1 = (object[])obj1;
			object[] arr2 = (object[])obj2;
			if(arr1.Length != arr2.Length)
				return false;
			else
			{
				for(int i = 0; i < arr1.Length; i++)
				{
					if(!CheckEquals(arr1[i], arr2[i]))
						return false;
				}
				return true;
			}
		}
		else
		{
			return obj1.Equals(obj2);
		}
	}
	
	#endregion
}