using UnityEngine;
using UnityEditor;

/// <summary>
/// Contains menu items to copy & paste transforms.
/// </summary>
public static class TransformMenu
{
	#region Private Fields

	private static bool _copied;
	private static Vector3 _localPosition;
	private static Quaternion _localRotation;
	private static Vector3 _localScale;
	private static Vector3 _globalPosition;
	private static Quaternion _globalRotation;

	#endregion
	#region Public Static Methods

	/// <summary>
	/// Copies both local and global transforms of the selected object.
	/// </summary>
	[MenuItem("Transform/Copy")]
	public static void CopyTransform()
	{
		if(!ValidateCopyTransform())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		var t = Selection.activeTransform;
		_localPosition = t.localPosition;
		_localRotation = t.localRotation;
		_localScale = t.localScale;
		_globalPosition = t.position;
		_globalRotation = t.rotation;
		_copied = true;
	}

	/// <summary>
	/// Returns whether the Copy command can be clicked.
	/// </summary>
	[MenuItem("Transform/Copy", true)]
	public static bool ValidateCopyTransform()
	{
		return Selection.activeTransform != null;
	}

	/// <summary>
	/// Pastes the local position, rotation, and scale values into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Local")]
	public static void PasteLocal()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Transform");
		t.localPosition = _localPosition;
		t.localRotation = _localRotation;
		t.localScale = _localScale;
	}

	/// <summary>
	/// Pastes the global position, rotation, and scale values into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Global")]
	public static void PasteGlobal()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Global Transform");
		t.position = _globalPosition;
		t.rotation = _globalRotation;
	}

	/// <summary>
	/// Pastes the local position value into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Local Position")]
	public static void PasteLocalPosition()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Position");
		t.localPosition = _localPosition;
	}

	/// <summary>
	/// Pastes the global position value into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Global Position")]
	public static void PasteGlobalPosition()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Global Position");
		t.position = _globalPosition;
	}

	/// <summary>
	/// Pastes the local rotation value into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Local Rotation")]
	public static void PasteLocalRotation()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Rotation");
		t.localRotation = _localRotation;
	}

	/// <summary>
	/// Pastes the global rotation value into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Global Rotation")]
	public static void PasteGlobalRotation()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Global Rotation");
		t.rotation = _globalRotation;
	}

	/// <summary>
	/// Pastes the local scale value into the selected objects.
	/// </summary>
	[MenuItem("Transform/Paste Local Scale")]
	public static void PasteLocalScale()
	{
		if(!ValidatePaste())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Scale");
		t.localScale = _localScale;
	}

	/// <summary>
	/// Returns whether any of the Paste commands can be clicked.
	/// </summary>
	[MenuItem("Transform/Paste Local", true)]
	[MenuItem("Transform/Paste Global", true)]
	[MenuItem("Transform/Paste Local Position", true)]
	[MenuItem("Transform/Paste Global Position", true)]
	[MenuItem("Transform/Paste Local Rotation", true)]
	[MenuItem("Transform/Paste Global Rotation", true)]
	[MenuItem("Transform/Paste Local Scale", true)]
	public static bool ValidatePaste()
	{
		return _copied && Selection.transforms.Length > 0;
	}

	/// <summary>
	/// Creates an empty child of the selected object.
	/// </summary>
	[MenuItem("Transform/Create Empty Child")]
	public static void CreateEmptyChild()
	{
		if(!ValidateCreateEmptyChild())
			return;

		if(!DemoVerifier.AllowUse)
		{
			TrialExpiredPopup.ShowTrialExpiredPopup();
			return;
		}

		Undo.RegisterSceneUndo("Create Empty Child");
		GameObject go = new GameObject();
		go.transform.parent = Selection.activeTransform;
		Selection.activeGameObject = go;
	}

	/// <summary>
	/// Returns whether create empty child can be clicked.
	/// </summary>
	[MenuItem("Transform/Create Empty Child", true)]
	public static bool ValidateCreateEmptyChild()
	{
		return Selection.activeTransform != null;
	}

	#endregion
}