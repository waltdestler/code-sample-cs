using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Stores a number of copied components of a game object.
/// </summary>
class RememberedObject
{
	#region Private Fields

	private int _objectInstanceID;
	private GameObject _cachedGameObject;
	private string _gameObjectName;
	private readonly Dictionary<string, CopiedVariable> _copiedVariables = new Dictionary<string, CopiedVariable>();
	private readonly Dictionary<ComponentKey, CopiedComponent> _copiedComponents = new Dictionary<ComponentKey, CopiedComponent>();
	private readonly List<RememberedObject> _rememberedChildren = new List<RememberedObject>();
	private bool _expandComponents;

	#endregion
	#region Properties

	public GameObject GameObject
	{
		get
		{
			if(!_cachedGameObject)
			{
				_cachedGameObject = null;
				foreach(GameObject go in Object.FindObjectsOfType(typeof(GameObject)))
				{
					if(go.GetInstanceID() == _objectInstanceID)
					{
						_cachedGameObject = go;
						break;
					}
				}
			}
			return _cachedGameObject;
		}
		private set
		{
			_objectInstanceID = value.GetInstanceID();
			_cachedGameObject = value;
			_gameObjectName = value.name;
		}
	}

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new RememberedObject with components copied from the specified object.
	/// </summary>
	public RememberedObject(GameObject obj, bool transformOnly = false)
	{
		GameObject = obj;
		if(transformOnly)
		{
			try
			{
				_copiedComponents[GetComponentKey(obj.transform)] = new CopiedComponent(obj.transform);
			}
			catch(Exception e)
			{
				Debug.LogError(e);
			}
		}
		else
		{
			foreach(ObjectVariableBase ov in ObjectVariables.GetObjectVariables(obj.GetType()))
				_copiedVariables[ov.VariableName] = new CopiedVariable(GameObject, ov);
			foreach(Component c in obj.GetComponents<Component>())
				_copiedComponents[GetComponentKey(c)] = new CopiedComponent(c);
			foreach(Transform t in obj.transform)
				_rememberedChildren.Add(new RememberedObject(t.gameObject));
		}
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Applies the remembered values to the object.
	/// </summary>
	public void Apply()
	{
		if(GameObject != null)
		{
			foreach(CopiedVariable cv in _copiedVariables.Values)
				cv.PasteInto(GameObject);
			foreach(var kvp in _copiedComponents)
				kvp.Value.PasteInto(GetComponent(kvp.Key));
			foreach(RememberedObject ro in _rememberedChildren)
				ro.Apply();
		}
		else
		{
			Debug.LogWarning("Rememberer: Game object could not be found.");
		}
	}

	public void DoGUI(out bool forget)
	{
		forget = false;
		GUILayout.BeginHorizontal();
		if(GameObject != null)
			EditorGUILayout.ObjectField(GameObject, typeof(GameObject), true);
		else
			GUILayout.Label(_gameObjectName);
		if(GameObject != null && GUILayout.Button("Apply", GUILayout.Width(50)))
		{
			Undo.RegisterUndo(GameObject, "Apply Remembered " + GameObject.name);
			Apply();
		}
		if(GUILayout.Button("Forget", GUILayout.Width(50)))
			forget = true;
		if(GUILayout.Button(_expandComponents ? "-" : "+", GUILayout.Width(25)))
			_expandComponents = !_expandComponents;
		GUILayout.EndHorizontal();
		if(_expandComponents)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.BeginVertical();
			foreach(var kvp in new List<KeyValuePair<string, CopiedVariable>>(_copiedVariables))
			{
				string key = kvp.Key;
				kvp.Value.DoGUI(injectGui: delegate(CopiedVariable cv)
				{
					if(GameObject != null && GUILayout.Button("Apply", GUILayout.Width(50)))
					{
						Undo.RegisterUndo(GameObject, "Apply Remembered " + ObjectNames.NicifyVariableName(cv.Name));
						cv.PasteInto(GameObject);
					}
					if(GUILayout.Button("Forget", GUILayout.Width(50)))
						_copiedVariables.Remove(key);
					GUI.enabled = false;
					GUILayout.Button("", GUILayout.Width(25));
					GUI.enabled = true;
				});
			}
			foreach(var kvp in new List<KeyValuePair<ComponentKey, CopiedComponent>>(_copiedComponents))
			{
				ComponentKey key = kvp.Key;
				Component c = GameObject != null ? GetComponent(key) : null;
				kvp.Value.DoGUI(variablesForgettable:true,
					injectGui:delegate(CopiedComponent cc)
					{
						if(c != null && GUILayout.Button("Apply", GUILayout.Width(50)))
						{
							Undo.RegisterUndo(c, "Apply Remembered " + ObjectNames.NicifyVariableName(key.GetType().Name));
							cc.PasteInto(c);
						}
						if(GUILayout.Button("Forget", GUILayout.Width(50)))
							_copiedComponents.Remove(key);
					},
					injectVariableGui:delegate(CopiedComponent cc, CopiedVariable cv)
					{
						if(c != null && GUILayout.Button("Apply", GUILayout.Width(50)))
						{
							Undo.RegisterUndo(c, "Apply Remembered " + ObjectNames.NicifyVariableName(cv.Name));
							cv.PasteInto(c);
						}
					});
			}
			foreach(var ro in new List<RememberedObject>(_rememberedChildren))
			{
				bool forgetChild;
				ro.DoGUI(out forgetChild);
				if(forgetChild)
					_rememberedChildren.Remove(ro);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
	}

	#endregion
	#region Private Methods

	/// <summary>
	/// Returns the ComponentKey for the specified component.
	/// </summary>
	private ComponentKey GetComponentKey(Component c)
	{
		Type type = c.GetType();
		return new ComponentKey(type, Array.IndexOf(GameObject.GetComponents(type), c));
	}

	/// <summary>
	/// Returns the Component at the specified ComponentKey.
	/// </summary>
	private Component GetComponent(ComponentKey ck)
	{
		return GameObject.GetComponents(ck.Type)[ck.Index];
	}

	#endregion
	#region Types

	/// <summary>
	/// Contains values needed to identify a component in a remembered game object.
	/// </summary>
	public struct ComponentKey
	{
		public Type Type;
		public int Index;

		public ComponentKey(Type type, int index)
		{
			Type = type;
			Index = index;
		}
	}

	#endregion
}