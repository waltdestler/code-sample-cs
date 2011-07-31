using UnityEngine;
using UnityEditor;

public static class MenuItems
{
	[MenuItem("Assets/Create/Constants")]
	public static void CreateConstants()
	{
		Constants asset = ScriptableObject.CreateInstance<Constants>();
		AssetDatabase.CreateAsset(asset, "Assets/Resources/Constants.asset");
	}
}