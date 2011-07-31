using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// A utility for remembering the values of an object at runtime.
/// </summary>
public class Rememberer : EditorWindow
{
	#region Private Fields

	private readonly Dictionary<GameObject, RememberedObject> _remembered = new Dictionary<GameObject, RememberedObject>();
	private Vector2 _scrollPos;

	#endregion
	#region Unity Methods

	public void OnGUI()
	{

		GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
		labelStyle.wordWrap = true;
		GUILayout.Label("The Rememberer will remember changes you've made to particular objects, even when playing. Once you've made your desired changes, simply click the Remember button to remember the current state of all selected objects. After the game has stopped, click Apply All to apply all remembered changes.", labelStyle);

		if(!DemoVerifier.AllowUse)
		{
			DemoVerifier.TrialExpiredGUI();
			return;
		}

		GUILayout.BeginHorizontal();

		// Remember button.
		if(Selection.transforms.Length == 0)
			GUI.enabled = false;
		if(GUILayout.Button("Remember"))
		{
			foreach(Transform t in Selection.transforms)
				_remembered[t.gameObject] = new RememberedObject(t.gameObject);
		}
		GUI.enabled = true;

		// Remember transform button.
		if(Selection.transforms.Length == 0)
			GUI.enabled = false;
		if(GUILayout.Button("Remember Transform"))
		{
			foreach(Transform t in Selection.transforms)
				_remembered[t.gameObject] = new RememberedObject(t.gameObject, true);
		}
		GUI.enabled = true;

		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();

		// Apply all button.
		if(_remembered.Count == 0)
			GUI.enabled = false;
		if(GUILayout.Button("Apply All"))
		{
			Undo.RegisterSceneUndo("Apply All Remembered");
			foreach(RememberedObject ro in _remembered.Values)
				ro.Apply();
		}
		GUI.enabled = true;

		// Forget all button.
		if(_remembered.Count == 0)
			GUI.enabled = false;
		if(GUILayout.Button("Forget All"))
		{
			_remembered.Clear();
		}
		GUI.enabled = true;

		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		// Draw each remembered object.
		_scrollPos = GUILayout.BeginScrollView(_scrollPos);
		foreach(RememberedObject ro in new List<RememberedObject>(_remembered.Values))
		{
			bool forget;
			ro.DoGUI(out forget);
			if(forget)
				_remembered.Remove(ro.GameObject);
		}
		GUILayout.EndScrollView();
	}

	public void OnSelectionChange()
	{
		Repaint();
	}

	#endregion
	#region Menu Items

	/// <summary>
	/// Shows the rememberer window.
	/// </summary>
	[MenuItem("Window/Rememberer")]
	public static void ShowRemembererWindow()
	{
		GetWindow<Rememberer>();
	}

	#endregion
}