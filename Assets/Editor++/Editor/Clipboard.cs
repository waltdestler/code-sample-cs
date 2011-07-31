using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Stores copied components whose values can be edited by the user.
/// </summary>
public class Clipboard : EditorWindow
{
	#region Private Fields

	private static Clipboard _current;
	private static readonly Dictionary<Type, CopiedComponent> _copiedComponents = new Dictionary<Type, CopiedComponent>();
	private Vector2 _scrollPos;

	#endregion
	#region Unity Methods

	public void OnEnable()
	{
		_current = this;
	}

	public void OnGUI()
	{
		if(!DemoVerifier.AllowUse)
		{
			DemoVerifier.TrialExpiredGUI();
			return;
		}

		GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
		labelStyle.wordWrap = true;
		GUILayout.Label("Right-click on a component name in the Inspector to copy values from or paste values into that component. Copied components will display in this window.", labelStyle);
		
		_scrollPos = GUILayout.BeginScrollView(_scrollPos);

		foreach(CopiedComponent cc in _copiedComponents.Values)
			cc.DoGUI(variablesToggleable:true);

		GUILayout.EndScrollView();
	}

	#endregion
	#region Menu Items

	/// <summary>
	/// Shows the clipboard.
	/// </summary>
	[MenuItem("Window/Clipboard")]
	public static void ShowClipboardWindow()
	{
		GetWindow<Clipboard>();
	}

	/// <summary>
	/// Copies the selected component.
	/// </summary>
	[MenuItem("CONTEXT/Component/Copy")]
	public static void CopyComponent(MenuCommand command)
	{
		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Component c = (Component)command.context;
		_copiedComponents[c.GetType()] = new CopiedComponent(c);
		if(_current != null)
			_current.Repaint();
	}

	/// <summary>
	/// Pastes the selected component.
	/// </summary>
	[MenuItem("CONTEXT/Component/Paste")]
	public static void PasteComponent(MenuCommand command)
	{
		if(ValidatePasteComponent(command))
		{
			if(!DemoVerifier.AllowUse)
			{
				TrialExpiredPopup.ShowTrialExpiredPopup();
				return;
			}

			Component c = (Component)command.context;
			Undo.RegisterUndo(c, "Paste " + ObjectNames.NicifyVariableName(c.GetType().Name) + " Component");
			_copiedComponents[c.GetType()].PasteInto(c);
		}
	}

	/// <summary>
	/// Returns whether the paste component command can be clicked.
	/// </summary>
	[MenuItem("CONTEXT/Component/Paste", true)]
	public static bool ValidatePasteComponent(MenuCommand command)
	{
		Component c = (Component)command.context;
		return _copiedComponents.ContainsKey(c.GetType());
	}

	#endregion
}