using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using WaltDestler.EditorPlusPlus;

/// <summary>
/// This static class contains all of the menu items made available by Enhanced Editor++.
/// </summary>
public static class EditorPlusPlusMenus
{
	#region Transform Menu

	private static bool _copied;
	private static Vector3 _localPosition;
	private static Quaternion _localRotation;
	private static Vector3 _localScale;
	private static Vector3 _globalPosition;
	private static Quaternion _globalRotation;

	/// <summary>
	/// Copies both local and global transforms of the selected object.
	/// </summary>
	[MenuItem("Edit/Copy Transform")]
	public static void CopyTransform()
	{
		if(!ValidateCopyTransform())
			return;

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
	[MenuItem("Edit/Copy Transform", true)]
	public static bool ValidateCopyTransform()
	{
		return Selection.activeTransform != null;
	}

	/// <summary>
	/// Pastes the local position, rotation, and scale values into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Local Transform")]
	public static void PasteLocal()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Transform");
		t.localPosition = _localPosition;
		t.localRotation = _localRotation;
		t.localScale = _localScale;
	}

	/// <summary>
	/// Pastes the global position, rotation, and scale values into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Global Transform")]
	public static void PasteGlobal()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Global Transform");
		t.position = _globalPosition;
		t.rotation = _globalRotation;
	}

	/// <summary>
	/// Pastes the local position value into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Local Position")]
	public static void PasteLocalPosition()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Position");
		t.localPosition = _localPosition;
	}

	/// <summary>
	/// Pastes the global position value into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Global Position")]
	public static void PasteGlobalPosition()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Global Position");
		t.position = _globalPosition;
	}

	/// <summary>
	/// Pastes the local rotation value into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Local Rotation")]
	public static void PasteLocalRotation()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Rotation");
		t.localRotation = _localRotation;
	}

	/// <summary>
	/// Pastes the global rotation value into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Global Rotation")]
	public static void PasteGlobalRotation()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Global Rotation");
		t.rotation = _globalRotation;
	}

	/// <summary>
	/// Pastes the local scale value into the selected objects.
	/// </summary>
	[MenuItem("Edit/Paste Local Scale")]
	public static void PasteLocalScale()
	{
		if(!ValidatePaste())
			return;

		Transform t = Selection.activeTransform;
		Undo.RegisterUndo(t, "Paste Local Scale");
		t.localScale = _localScale;
	}

	/// <summary>
	/// Returns whether any of the Paste commands can be clicked.
	/// </summary>
	[MenuItem("Edit/Paste Local Transform", true)]
	[MenuItem("Edit/Paste Global Transform", true)]
	[MenuItem("Edit/Paste Local Position", true)]
	[MenuItem("Edit/Paste Global Position", true)]
	[MenuItem("Edit/Paste Local Rotation", true)]
	[MenuItem("Edit/Paste Global Rotation", true)]
	[MenuItem("Edit/Paste Local Scale", true)]
	public static bool ValidatePaste()
	{
		return _copied && Selection.transforms.Length > 0;
	}

	/// <summary>
	/// Creates an empty child of the selected object.
	/// </summary>
	[MenuItem("GameObject/Create Empty Child")]
	public static void CreateEmptyChild()
	{
		if(!ValidateCreateEmptyChild())
			return;

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
	#region Clipboard

	/// <summary>
	/// Shows the clipboard.
	/// </summary>
	[MenuItem("Window/Clipboard")]
	public static void ShowClipboardWindow()
	{
		EditorWindow.GetWindow<Clipboard>(false, "Clipboard");
	}

	/// <summary>
	/// Copies the selected component.
	/// </summary>
	[MenuItem("CONTEXT/Component/Copy")]
	public static void CopyComponent(MenuCommand command)
	{
		Clipboard.CopyComponent((Component)command.context);
	}

	/// <summary>
	/// Pastes the selected component.
	/// </summary>
	[MenuItem("CONTEXT/Component/Paste")]
	public static void PasteComponent(MenuCommand command)
	{
		if(ValidatePasteComponent(command))
			Clipboard.PasteComponent((Component)command.context);
	}

	/// <summary>
	/// Returns whether the paste component command can be clicked.
	/// </summary>
	[MenuItem("CONTEXT/Component/Paste", true)]
	public static bool ValidatePasteComponent(MenuCommand command)
	{
		Component c = (Component)command.context;
		return Clipboard.CanPasteInto(c);
	}

	#endregion
	#region Component Reorderer

	/// <summary>
	/// Shows the ComponentReorderer window.
	/// </summary>
	[MenuItem("Window/Component Reorderer (BETA)")]
	public static void ShowComponentReordererWindow()
	{
		EditorWindow.GetWindow<ComponentReorderer>(false, "Component Reorderer");
	}

	#endregion
	#region Help Menu

	/// <summary>
	/// Shows the user's guide webpage.
	/// </summary>
	[MenuItem("Help/Enhanced Editor++/User's Guide")]
	public static void ShowUsersGuide()
	{
		Application.OpenURL("http://www.waltdestler.com/editorplusplus/usersguide.html");
	}

	/// <summary>
	/// Shows the support contact webpage.
	/// </summary>
	[MenuItem("Help/Enhanced Editor++/Contact Support")]
	public static void ShowContactSupport()
	{
		Application.OpenURL("http://www.waltdestler.com/editorplusplus/support.html");
	}

	/// <summary>
	/// Shows the package in the asset store.
	/// </summary>
	[MenuItem("Help/Enhanced Editor++/Asset Store")]
	public static void ShowAssetStore()
	{
		AssetStore.Open("http://u3d.as/content/135");
	}

	/// <summary>
	/// Shows the about window.
	/// </summary>
	[MenuItem("Help/Enhanced Editor++/About")]
	public static void ShowAbout()
	{
		EditorWindow.GetWindow<HelpMenu>(true, "About Enhanced Editor++");
	}

	#endregion
	#region Rememberer

	/// <summary>
	/// Shows the rememberer window.
	/// </summary>
	[MenuItem("Window/Rememberer")]
	public static void ShowRemembererWindow()
	{
		EditorWindow.GetWindow<Rememberer>(false, "Rememberer");
	}

	/// <summary>
	/// Remembers the game object connected to the specified component.
	/// </summary>
	[MenuItem("CONTEXT/Component/Remember Game Object")]
	public static void RememberGameObject(MenuCommand command)
	{
		Component c = (Component)command.context;
		GameObject go = c.gameObject;
		Rememberer.RememberGameObject(go);
	}

	/// <summary>
	/// Remembers the specified transform.
	/// </summary>
	[MenuItem("CONTEXT/Component/Remember Transform")]
	public static void RememberTransform(MenuCommand command)
	{
		Component c = (Component)command.context;
		GameObject go = c.gameObject;
		Rememberer.RememberTransform(go);
	}

	#endregion
	#region Find Usages

	/// <summary>
	/// Finds the usages of the selected object in the scene.
	/// </summary>
	[MenuItem("Assets/Find Usages In Scene")]
	public static void FindUsagesInScene()
	{
		UsagesFinder.ShowWindowForScene(Selection.activeObject);
	}

	/// <summary>
	/// Returns whether the "Find Usages/In Scene" menu item can be clicked.
	/// </summary>
	[MenuItem("Assets/Find Usages In Scene", true)]
	public static bool ValidateFindUsagesInScene()
	{
		return Selection.activeObject != null;
	}

	/// <summary>
	/// Finds the usages of the selected object in the scene.
	/// </summary>
	[MenuItem("Assets/Find Usages In Assets")]
	public static void FindUsagesInAssets()
	{
		UsagesFinder.ShowWindowForAssets(Selection.activeObject);
	}

	/// <summary>
	/// Returns whether the "Find Usages/In Assets" menu item can be clicked.
	/// </summary>
	[MenuItem("Assets/Find Usages In Assets", true)]
	public static bool ValidateFindUsagesInAssets()
	{
		return Selection.activeObject != null && AssetDatabase.Contains(Selection.activeObject);
	}

	/// <summary>
	/// Finds the usages of the selected object in the scene.
	/// </summary>
	[MenuItem("CONTEXT/Object/Find Usages in Scene")]
	public static void ContextFindUsagesInScene(MenuCommand command)
	{
		UsagesFinder.ShowWindowForScene(command.context);
	}

	/// <summary>
	/// Finds the usages of the selected object in the scene.
	/// </summary>
	[MenuItem("CONTEXT/Object/Find Usages in Assets")]
	public static void ContextFindUsagesInAssets(MenuCommand command)
	{
		UsagesFinder.ShowWindowForAssets(command.context);
	}

	/// <summary>
	/// Returns whether the "Find Usages in Assets" context item can be clicked.
	/// </summary>
	[MenuItem("CONTEXT/Object/Find Usages in Assets", true)]
	public static bool ValidateContextFindUsagesInAssets(
		MenuCommand command)
	{
		return AssetDatabase.Contains(command.context);
	}

	#endregion
}
