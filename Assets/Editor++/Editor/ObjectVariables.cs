using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Helper class to return all object variables for a given type.
/// </summary>
static class ObjectVariables
{
	#region Private Static Fields

	private static List<MethodInfo> _attributedMethods;
	private static readonly Dictionary<Type, List<ObjectVariableBase>> _variablesForType = new Dictionary<Type, List<ObjectVariableBase>>();

	#endregion
	#region Public Static Methods

	/// <summary>
	/// Returns all of the object variables for the specified type.
	/// </summary>
	public static IEnumerable<ObjectVariableBase> GetObjectVariables(Type type)
	{
		if(_attributedMethods == null)
			FindAttributedMethods();

		if(!_variablesForType.ContainsKey(type))
		{
			List<ObjectVariableBase> list = new List<ObjectVariableBase>();
			_variablesForType.Add(type, list);
			foreach(MethodInfo mi in _attributedMethods)
			{
				bool foundValidAttribute = false;
				foreach(ObjectVariablesAttribute attr in mi.GetCustomAttributes(typeof(ObjectVariablesAttribute), false))
				{
					if(IsTypeOrSubclassOf(type, attr.ObjectType))
					{
						foundValidAttribute = true;
						break;
					}
				}

				if(foundValidAttribute)
					list.AddRange((IEnumerable<ObjectVariableBase>)mi.Invoke(null, new[] { type }));
			}
		}

		return _variablesForType[type];
	}

	#endregion
	#region Private Static Methods

	/// <summary>
	/// Fills the _attributedMethods list with all of the methods which may be called to get the variables for a type of object.
	/// </summary>
	private static void FindAttributedMethods()
	{
		_attributedMethods = new List<MethodInfo>();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach(Assembly assembly in assemblies)
		{
			foreach(Type type in assembly.GetTypes())
			{
				foreach(MethodInfo mi in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if(Attribute.IsDefined(mi, typeof(ObjectVariablesAttribute)) &&
						mi.ReturnType == typeof(IEnumerable<ObjectVariableBase>))
					{
						_attributedMethods.Add(mi);
					}
				}
			}
		}
	}

	/// <summary>
	/// Returns whether type is of the same type or a subclass of baseType.
	/// </summary>
	private static bool IsTypeOrSubclassOf(Type type, Type baseType)
	{
		return type == baseType || type.IsSubclassOf(baseType);
	}

	#endregion
}