using UnityEngine;
using UnityEditor;

/// <summary>
/// A custom editor that allows the start and end points of a PositionAction to be easily set.
/// </summary>
[CustomEditor(typeof(PositionAction))]
public class PositionActionEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PositionAction pa = (PositionAction)target;
		if(GUILayout.Button("Set Start as Current"))
			pa.StartPos = pa.transform.position;
		if(GUILayout.Button("Set End as Current"))
			pa.EndPos = pa.transform.position;
		if(GUILayout.Button("Set Current as Start"))
			pa.transform.position = pa.StartPos;
		if(GUILayout.Button("Set Current as End"))
			pa.transform.position = pa.EndPos;
	}
}