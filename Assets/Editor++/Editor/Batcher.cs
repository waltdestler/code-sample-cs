using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// When enabled, any changes made to a game object will be applied to all selected game objects.
/// </summary>
public class Batcher : EditorWindow
{
	#region Private Fields

	private readonly Dictionary<GameObject, TrackedGameObject> _trackedObjects = new Dictionary<GameObject, TrackedGameObject>();
	private bool _wasPlaying;

	#endregion
	#region Static Properties

	/// <summary>
	/// Gets or sets whether batching is enabled.
	/// </summary>
	public static bool Enabled
	{
		get { return EditorPrefs.GetBool("Editor++.Batcher.Enabled", false); }
		set { EditorPrefs.SetBool("Editor++.Batcher.Enabled", value); }
	}

	/// <summary>
	/// Gets or sets whether transform batching is enabled.
	/// </summary>
	public static bool TransformEnabled
	{
		get { return EditorPrefs.GetBool("Editor++.Batcher.TransformEnabled", false); }
		set { EditorPrefs.SetBool("Editor++.Batcher.TransformEnabled", value); }
	}

	#endregion
	#region Unity Methods

	public void OnEnable()
	{
		EditorApplication.update += DoUpdate;
	}

	public void OnDisable()
	{
		EditorApplication.update -= DoUpdate;
	}

	public void OnGUI()
	{
		GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
		labelStyle.wordWrap = true;
		GUILayout.Label("When enabled, any changes made to an object will be made to ALL selected objects.", labelStyle);

		if(!DemoVerifier.AllowUse)
		{
			DemoVerifier.TrialExpiredGUI();
			return;
		}

		if(Application.isPlaying)
		{
			Color old = GUI.contentColor;
			GUI.color = Color.yellow;
			GUILayout.Label("Batching is always disabled while playing.");
			GUI.color = old;
		}
		else if(Enabled)
		{
			Color old = GUI.contentColor;
			GUI.contentColor = Color.green;
			GUILayout.BeginHorizontal();
			GUILayout.Label("Batching is ENABLED.");
			GUI.contentColor = old;
			if(GUILayout.Button("Disable", GUILayout.Width(100)))
				Enabled = false;
			GUILayout.EndHorizontal();

			if(TransformEnabled)
			{
				old = GUI.contentColor;
				GUI.contentColor = Color.green;
				GUILayout.BeginHorizontal();
				GUILayout.Label("Transform batching is ENABLED.");
				GUI.contentColor = old;
				if(GUILayout.Button("Disable", GUILayout.Width(100)))
					TransformEnabled = false;
				GUILayout.EndHorizontal();
				GUI.contentColor = Color.red;
				GUILayout.Label("WARNING: Transform batching will cause problems if you move multiple objects at once using the Scene view.", labelStyle);
				GUI.contentColor = old;
			}
			else
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Transform batching is DISABLED.");
				if(GUILayout.Button("Enable", GUILayout.Width(100)))
					TransformEnabled = true;
				GUILayout.EndHorizontal();
			}
		}
		else
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Batching is DISABLED.");
			if(GUILayout.Button("Enable", GUILayout.Width(100)))
				Enabled = true;
			GUILayout.EndHorizontal();
		}
	}

	public void Update()
	{
		if(Application.isPlaying != _wasPlaying)
		{
			Repaint();
			_wasPlaying = Application.isPlaying;
		}
	}

	#endregion
	#region Private Methods

	/// <summary>
	/// Checks for any modifications to the active game object and applies them to the other selected game objects.
	/// </summary>
	private void DoUpdate()
	{
		// Don't run if not enabled, application is playing, or there aren't more than one object selected.
		if(!Enabled || Application.isPlaying || Selection.gameObjects.Length <= 1)
		{
			_trackedObjects.Clear();
			return;
		}

		// Remove any tracked objects not currently selected.
		foreach(GameObject go in new List<GameObject>(_trackedObjects.Keys))
		{
			if(!Selection.gameObjects.Contains(go))
				_trackedObjects.Remove(go);
		}

		// Add any selected objects not being tracked.
		foreach(GameObject go in Selection.gameObjects)
		{
			if(!_trackedObjects.ContainsKey(go))
				_trackedObjects.Add(go, new TrackedGameObject(go));
		}

		// Update all tracked objects.
		IEnumerable<TrackedGameObject.ObjectChange> _changesToActive = null;
		List<TrackedGameObject.ObjectChange> _otherChanges = new List<TrackedGameObject.ObjectChange>();
		foreach(TrackedGameObject tgo in _trackedObjects.Values)
		{
			if(tgo.GameObject == Selection.activeGameObject)
				_changesToActive = tgo.Update();
			else
				_otherChanges.AddRange(tgo.Update());
		}

		// Apply changes across all selected objects.
		if(_changesToActive != null)
		{
			bool firstChange = true;
			bool madeChanges = false;
			foreach(var change in _changesToActive)
			{
				// Create an undo snapshot which will be registered if any changes are actually made.
				if(firstChange)
				{
					UnityEngine.Object[] objectsToUndo = new List<UnityEngine.Object>(_trackedObjects.Values.Select(o => (UnityEngine.Object)o.GameObject)).ToArray();
					Undo.SetSnapshotTarget(objectsToUndo, "Batch Edit");
					Undo.CreateSnapshot();
					firstChange = false;
				}

				foreach(TrackedGameObject tgo in _trackedObjects.Values)
				{
					if(tgo.GameObject == Selection.activeGameObject)
						continue;

					// Change variable?
					if(change is TrackedGameObject.ObjectVariableChange)
					{
						var varChange = (TrackedGameObject.ObjectVariableChange)change;
						TrackedVariable tv = tgo[varChange.TrackedVariable];
						if(tv != null && !tv.Changed)
						{
							tv.SetValue(varChange.NewValue);
							madeChanges = true;
						}
					}

					// Change transform variable?
					else if(change is TrackedGameObject.ObjectTransformVariableChange)
					{
						if(TransformEnabled)
						{
							var varChange = (TrackedGameObject.ObjectTransformVariableChange)change;
							tgo.TrackedTransform.SetValue(varChange.TransformVariable, varChange.NewValue);
							madeChanges = true;
						}
					}

					// Add component?
					else if(change is TrackedGameObject.ObjectNewComponentChange)
					{
						var newCompChange = (TrackedGameObject.ObjectNewComponentChange)change;
						if(!CheckNewComponentChangeExists(_otherChanges, newCompChange.ComponentData.ComponentType, tgo))
						{
							tgo.AddComponent(newCompChange.ComponentData);
							madeChanges = true;
						}
					}

					// Remove component?
					else if(change is TrackedGameObject.ObjectRemoveComponentChange)
					{
						var remCompChange = (TrackedGameObject.ObjectRemoveComponentChange)change;
						if(!CheckRemoveComponentChangeExists(_otherChanges, remCompChange.ComponentType, remCompChange.ComponentIndex, tgo))
						{
							tgo.RemoveComponent(remCompChange.ComponentType, remCompChange.ComponentIndex);
							madeChanges = true;
						}
					}
				}
			}

			// Register undo if any changes were made.
			if(madeChanges)
				Undo.RegisterSnapshot();
			Undo.ClearSnapshotTarget();
		}
	}

	/// <summary>
	/// Returns whether the specified list of changes contains a new component change that matches the specified component type and tracked game object.
	/// </summary>
	private static bool CheckNewComponentChangeExists(IEnumerable<TrackedGameObject.ObjectChange> changes, Type componentType, TrackedGameObject tgo)
	{
		foreach(var change in changes)
		{
			if(change is TrackedGameObject.ObjectNewComponentChange)
			{
				var newCompChange = (TrackedGameObject.ObjectNewComponentChange)change;
				if(newCompChange.ComponentData.ComponentType == componentType && newCompChange.Source == tgo)
					return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Returns whether the specified list of changes contains a remove component change that matches the specified component type and index.
	/// </summary>
	private static bool CheckRemoveComponentChangeExists(IEnumerable<TrackedGameObject.ObjectChange> changes, Type componentType, int componentIndex, TrackedGameObject tgo)
	{
		foreach(var change in changes)
		{
			if(change is TrackedGameObject.ObjectRemoveComponentChange)
			{
				var remCompChange = (TrackedGameObject.ObjectRemoveComponentChange)change;
				if(remCompChange.ComponentType == componentType && remCompChange.ComponentIndex == componentIndex && remCompChange.Source == tgo)
					return true;
			}
		}

		return false;
	}

	#endregion
	#region Menu Items

	/// <summary>
	/// Shows the batcher window.
	/// </summary>
	[MenuItem("Window/Batcher")]
	public static void ShowBatcherWindow()
	{
		GetWindow<Batcher>();
	}

	#endregion
}