using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

delegate void InjectCopiedComponentGuiCallback(CopiedComponent cc);
delegate void InjectCopiedComponentVariableGuiCallback(CopiedComponent cc, CopiedVariable cv);

/// <summary>
/// Stores data about values copied from a component.
/// </summary>
class CopiedComponent
{
	#region Private Fields

	private readonly Dictionary<string, CopiedVariable> _copiedVariables = new Dictionary<string, CopiedVariable>();
	private static readonly Dictionary<string, bool> _enabledVariables = new Dictionary<string, bool>();
	private bool _expandFields;

	#endregion
	#region Properties

	public Type ComponentType { get; private set; }

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new CopiedComponent that stores values copied from the specified component at the time of construction.
	/// </summary>
	public CopiedComponent(Component component)
	{
		if(component == null)
			throw new ArgumentNullException("component");

		ComponentType = component.GetType();

		foreach(ObjectVariableBase ov in ObjectVariables.GetObjectVariables(ComponentType))
			_copiedVariables[ov.VariableName] = new CopiedVariable(component, ov);
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Pastes all of the copied values into the specified component.
	/// </summary>
	public void PasteInto(Component component)
	{
		if(component == null)
			throw new ArgumentNullException("component");
		if(ComponentType != component.GetType())
			throw new ArgumentException("The specified component is not the same type as the copied component.", "component");

		foreach(CopiedVariable cv in _copiedVariables.Values)
		{
			if(GetVariableEnabled(cv.Name))
				cv.PasteInto(component);
		}
	}

	/// <summary>
	/// Displays a GUI that allows modification of the copied component.
	/// </summary>
	public void DoGUI(bool variablesForgettable = false, bool variablesToggleable = false,
		InjectCopiedComponentGuiCallback injectGui = null, InjectCopiedComponentVariableGuiCallback injectVariableGui = null)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(ObjectNames.NicifyVariableName(ComponentType.Name), EditorStyles.boldLabel);
		if(injectGui != null)
			injectGui(this);
		if(GUILayout.Button(_expandFields ? "-" : "+", GUILayout.Width(25)))
			_expandFields = !_expandFields;
		GUILayout.EndHorizontal();
		if(_expandFields)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.BeginVertical();
			foreach(var kvp in new List<KeyValuePair<string, CopiedVariable>>(_copiedVariables))
			{
				string key = kvp.Key;

				GUI.enabled = GetVariableEnabled(key);
				kvp.Value.DoGUI(delegate(CopiedVariable cv)
				{
					GUI.enabled = true;
					if(injectVariableGui != null)
						injectVariableGui(this, cv);
					if(variablesForgettable && GUILayout.Button("Forget", GUILayout.Width(50)))
						_copiedVariables.Remove(key);
					if(variablesToggleable)
						SetVariableEnabled(key, GUILayout.Toggle(GetVariableEnabled(key), "", GUILayout.Width(15)));
					GUI.enabled = false;
					GUILayout.Button("", GUILayout.Width(25));
					GUI.enabled = true;
				});
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
	}

	/// <summary>
	/// Forgets the specified copied variable.
	/// </summary>
	public void ForgetVariable(CopiedVariable cv)
	{
		_copiedVariables.Remove(cv.Name);
	}
	
	#endregion
	#region Private Methods

	/// <summary>
	/// Gets whether the specified variable is enabled.
	/// </summary>
	private static bool GetVariableEnabled(string name)
	{
		if(_enabledVariables.ContainsKey(name))
			return _enabledVariables[name];
		else
			return true;
	}

	/// <summary>
	/// Sets whether the specified variable is enabled.
	/// </summary>
	private static void SetVariableEnabled(string name, bool enabled)
	{
		_enabledVariables[name] = enabled;
	}

	#endregion
}