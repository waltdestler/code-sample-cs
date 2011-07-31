using System;
using UnityEngine;
using UnityEditor;

delegate void InjectCopiedVariableGuiCallback(CopiedVariable cv);

/// <summary>
/// Stores the value of a copied variable displays a GUI that allows that copied value to be edited.
/// </summary>
class CopiedVariable
{
	#region Private Fields

	private readonly ObjectVariableBase _ov;
	private object _value;

	#endregion
	#region Properties

	public string Name
	{
		get { return _ov.VariableName; }
	}

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new CopiedVariable from an object variable.
	/// </summary>
	public CopiedVariable(object obj, ObjectVariableBase ov)
	{
		if(obj == null)
			throw new ArgumentNullException("obj");
		if(ov == null)
			throw new ArgumentNullException("ov");

		_ov = ov;
		_value = ov.GetValue(obj);
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Pastes the copied variable into the specified object.
	/// </summary>
	public void PasteInto(object obj)
	{
		if(obj == null)
			throw new ArgumentNullException("obj");

		_ov.SetValue(obj, _value);
	}

	/// <summary>
	/// Displays a GUI that allows modification of the copied value.
	/// </summary>
	public void DoGUI(InjectCopiedVariableGuiCallback injectGui = null)
	{
		Type type = _ov.VariableType;
		string name = ObjectNames.NicifyVariableName(_ov.VariableName);
		GUILayout.BeginHorizontal();
		if(IsTypeOrSubclassOf(type, typeof(UnityEngine.Object)))
			_value = EditorGUILayout.ObjectField(name, (UnityEngine.Object)_value, type, true);
		else if(type == typeof(bool))
			_value = EditorGUILayout.Toggle(name, (bool)_value);
		else if(type == typeof(string))
			_value = EditorGUILayout.TextField(name, (string)_value);
		else if(type == typeof(float))
			_value = EditorGUILayout.FloatField(name, (float)_value);
		else if(type == typeof(int))
			_value = EditorGUILayout.IntField(name, (int)_value);
		else if(type == typeof(Vector2))
			_value = EditorGUILayout.Vector2Field(name, (Vector2)_value);
		else if(type == typeof(Vector3))
			_value = EditorGUILayout.Vector3Field(name, (Vector3)_value);
		else if(type == typeof(Vector4))
			_value = EditorGUILayout.Vector4Field(name, (Vector4)_value);
		else if(type == typeof(Rect))
			_value = EditorGUILayout.RectField(name, (Rect)_value);
		else if(type == typeof(Color))
			_value = EditorGUILayout.ColorField(name, (Color)_value);
		else if(type == typeof(AnimationCurve))
			_value = EditorGUILayout.CurveField(name, (AnimationCurve)_value);
		else
			EditorGUILayout.LabelField(name, _value.ToString());
		if(injectGui != null)
			injectGui(this);
		GUILayout.EndHorizontal();
	}

	#endregion
	#region Private Methods

	/// <summary>
	/// Returns whether type is of the same type or a subclass of baseType.
	/// </summary>
	private static bool IsTypeOrSubclassOf(Type type, Type baseType)
	{
		return type == baseType || type.IsSubclassOf(baseType);
	}

	#endregion
}