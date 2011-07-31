using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Tracks the variables and components of a game object.
/// </summary>
class TrackedGameObject
{
	#region Private Fields

	private readonly Dictionary<string, TrackedVariable> _trackedVariables = new Dictionary<string, TrackedVariable>();
	private readonly Dictionary<Type, List<TrackedComponent>> _trackedComponents = new Dictionary<Type, List<TrackedComponent>>();

	#endregion
	#region Properties

	/// <summary>
	/// The game object that is being tracked.
	/// </summary>
	public GameObject GameObject { get; private set; }

	/// <summary>
	/// Gets the specified tracked component, or null if it is not being tracked.
	/// </summary>
	public TrackedComponent this[TrackedComponentKey key]
	{
		get
		{
			if(_trackedComponents.ContainsKey(key.Type))
			{
				var list = _trackedComponents[key.Type];
				if(key.Index < list.Count)
					return list[key.Index];
				else
					return null;
			}
			else
			{
				return null;
			}
		}
	}

	/// <summary>
	/// Gets the specified tracked variable, or null if it is not being tracked.
	/// </summary>
	public TrackedVariable this[TrackedVariableKey key]
	{
		get
		{
			if(key.Component.Type == null)
			{
				if(_trackedVariables.ContainsKey(key.VariableName))
					return _trackedVariables[key.VariableName];
				else
					return null;
			}
			else
			{
				TrackedComponent tc = this[key.Component];
				if(tc != null)
					return tc[key.VariableName];
				else
					return null;
			}
		}
	}

	/// <summary>
	/// Gets the TrackedTransform object that tracks the game object's transform.
	/// </summary>
	public TrackedTransform TrackedTransform { get; private set; }

	#endregion
	#region Constructors

	/// <summary>
	/// Creates a new TrackedGameObject that tracks the variables and components of the specified game object.
	/// </summary>
	public TrackedGameObject(GameObject obj)
	{
		if(obj == null)
			throw new ArgumentNullException("obj");

		GameObject = obj;
		foreach(ObjectVariableBase ov in ObjectVariables.GetObjectVariables(obj.GetType()))
			_trackedVariables[ov.VariableName] = new TrackedVariable(GameObject, ov);
		foreach(Component c in obj.GetComponents<Component>())
		{
			Type type = c.GetType();
			if(type == typeof(Transform))
				continue;
			if(!_trackedComponents.ContainsKey(type))
			{
				List<TrackedComponent> list = new List<TrackedComponent> { new TrackedComponent(c) };
				_trackedComponents.Add(type, list);
			}
			else
			{
				_trackedComponents[type].Add(new TrackedComponent(c));
			}
		}
		TrackedTransform = new TrackedTransform(obj.transform);
	}

	#endregion
	#region Public Methods

	/// <summary>
	/// Updates the current tracked state of the game object.
	/// </summary>
	/// <returns>Any changes that have been made to the game object.</returns>
	public IEnumerable<ObjectChange> Update()
	{
		List<ObjectChange> _changes = new List<ObjectChange>();

		// Check for newly-added components.
		Component[] components = GameObject.GetComponents<Component>();
		foreach(Component c in components)
		{
			Type type = c.GetType();
			if(type == typeof(Transform))
				continue;
			Component c2 = c;
			if(!_trackedComponents.ContainsKey(type) || !_trackedComponents[type].Exists(tc => tc.Component == c2))
			{
				_changes.Add(new ObjectNewComponentChange(this, new CopiedComponent(c)));
				if(!_trackedComponents.ContainsKey(type))
				{
					List<TrackedComponent> list = new List<TrackedComponent> { new TrackedComponent(c) };
					_trackedComponents.Add(type, list);
				}
				else
				{
					_trackedComponents[type].Add(new TrackedComponent(c));
				}
			}
		}

		// Check for removed components.
		foreach(Type type in _trackedComponents.Keys)
		{
			var list = _trackedComponents[type];
			for(int i = 0; i < list.Count; i++)
			{
				TrackedComponent tc = list[i];
				if(!components.Contains(tc.Component))
				{
					_changes.Add(new ObjectRemoveComponentChange(this, type, i));
					_trackedComponents[type].RemoveAt(i);
					i--;
				}
			}
		}

		// Update tracked variables.
		foreach(string name in _trackedVariables.Keys)
		{
			TrackedVariable tv = _trackedVariables[name];
			if(tv.Update())
			{
				_changes.Add(new ObjectVariableChange(
					this,
					new TrackedVariableKey(null, 0, name),
					tv.CurrentValue));
			}
		}

		// Update tracked components.
		foreach(Type componentType in _trackedComponents.Keys)
		{
			var list = _trackedComponents[componentType];
			for(int i = 0; i < list.Count; i++)
			{
				TrackedComponent tc = list[i];
				foreach(var tv in tc.Update())
				{
					_changes.Add(new ObjectVariableChange(
						this,
						new TrackedVariableKey(componentType, i, tv.Name),
						tv.CurrentValue));
				}
			}
		}

		// Update tracked transform.
		foreach(TrackedTransform.TransformVariable v in TrackedTransform.Update())
		{
			_changes.Add(new ObjectTransformVariableChange(this, v, TrackedTransform.GetCurrentValue(v)));
		}

		return _changes;
	}

	/// <summary>
	/// Adds a new component to the game object.
	/// </summary>
	public void AddComponent(CopiedComponent componentData)
	{
		Component c = GameObject.AddComponent(componentData.ComponentType);
		componentData.PasteInto(c);
		if(_trackedComponents.ContainsKey(componentData.ComponentType))
			_trackedComponents[componentData.ComponentType].Add(new TrackedComponent(c));
		else
			_trackedComponents[componentData.ComponentType] = new List<TrackedComponent> { new TrackedComponent(c) };
	}

	/// <summary>
	/// Removes the specified component from the game object, if it exists.
	/// </summary>
	public void RemoveComponent(Type componentType, int componentIndex)
	{
		if(_trackedComponents.ContainsKey(componentType))
		{
			var list = _trackedComponents[componentType];
			if(componentIndex < list.Count)
			{
				Object.DestroyImmediate(list[componentIndex].Component);
				list.RemoveAt(componentIndex);
			}
		}
	}

	#endregion
	#region Types

	/// <summary>
	/// Contains values needed to identify a tracked component in a tracked game object.
	/// </summary>
	public struct TrackedComponentKey
	{
		public Type Type;
		public int Index;

		public TrackedComponentKey(Type type, int index)
		{
			Type = type;
			Index = index;
		}
	}

	/// <summary>
	/// Contains values needed to identify a tracked variable in a tracked game object or one of its components.
	/// </summary>
	public struct TrackedVariableKey
	{
		public TrackedComponentKey Component;
		public string VariableName;

		public TrackedVariableKey(TrackedComponentKey component, string variableName)
		{
			Component = component;
			VariableName = variableName;
		}

		public TrackedVariableKey(Type componentType, int componentIndex, string variableName)
		{
			Component = new TrackedComponentKey(componentType, componentIndex);
			VariableName = variableName;
		}
	}

	/// <summary>
	/// Base class for storing changes to game objects.
	/// </summary>
	public abstract class ObjectChange
	{
		public TrackedGameObject Source;

		protected ObjectChange(TrackedGameObject source)
		{
			Source = source;
		}
	}

	/// <summary>
	/// Stores data for a changed variable of a game object.
	/// </summary>
	public class ObjectVariableChange : ObjectChange
	{
		public TrackedVariableKey TrackedVariable;
		public object NewValue;

		public ObjectVariableChange(TrackedGameObject source, TrackedVariableKey tv, object newValue)
			: base(source)
		{
			TrackedVariable = tv;
			NewValue = newValue;
		}
	}

	/// <summary>
	/// Stores data for a changed transform variable.
	/// </summary>
	public class ObjectTransformVariableChange : ObjectChange
	{
		public TrackedTransform.TransformVariable TransformVariable;
		public float NewValue;

		public ObjectTransformVariableChange(TrackedGameObject source, TrackedTransform.TransformVariable v, float newValue)
			: base(source)
		{
			TransformVariable = v;
			NewValue = newValue;
		}
	}

	/// <summary>
	/// Stores data for a new component.
	/// </summary>
	public class ObjectNewComponentChange : ObjectChange
	{
		public CopiedComponent ComponentData;

		public ObjectNewComponentChange(TrackedGameObject source, CopiedComponent componentData)
			: base(source)
		{
			ComponentData = componentData;
		}
	}

	/// <summary>
	/// Stores data about a component that has been removed.
	/// </summary>
	public class ObjectRemoveComponentChange : ObjectChange
	{
		public Type ComponentType;
		public int ComponentIndex;

		public ObjectRemoveComponentChange(TrackedGameObject source, Type componentType, int componentIndex)
			: base(source)
		{
			ComponentType = componentType;
			ComponentIndex = componentIndex;
		}
	}

	#endregion
}