using UnityEngine;
using UnityEditor;

/// <summary>
/// Contains various custom menu items for the editor.
/// </summary>
public static class MenuItems
{
	/// <summary>
	/// Creates the Constants asset.
	/// </summary>
	[MenuItem("Assets/Create/Constants")]
	public static void CreateConstants()
	{
		Constants asset = ScriptableObject.CreateInstance<Constants>();
		AssetDatabase.CreateAsset(asset, "Assets/Resources/Constants.asset");
	}
}