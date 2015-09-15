using UnityEngine;
using UnityEditor;

public class MegamanUtilities : EditorWindow
{
	public LayerMask groundLayer;
	public LayerMask wallLayer;
	public const string defaultGroundObjectsName = "Collider - Ground";
	public const string defaultWallObjectsName = "Collider - Wall";

	[MenuItem ("MegamanX/Utilities")]
	static void Init () {
		MegamanUtilities window = (MegamanUtilities)EditorWindow.GetWindow (typeof (MegamanUtilities));
		window.Show();
	}
	
	void OnGUI ()
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Ground layer: ");
		groundLayer = EditorGUILayout.LayerField (groundLayer.value);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Wall layer: ");
		wallLayer = EditorGUILayout.LayerField (wallLayer.value);
		GUILayout.EndHorizontal ();

		if(GUILayout.Button("Set default Ground-layered object names"))
		{
			SetDefaultSettingsForGroundLayeredObjects();
		}
	}

	void SetDefaultSettingsForGroundLayeredObjects()
	{
		Collider2D[] allSceneColliders = Resources.FindObjectsOfTypeAll<Collider2D> ();

		foreach(Collider2D localCollider in allSceneColliders)
		{
			if(localCollider.gameObject.layer == groundLayer.value)
			{
				localCollider.gameObject.name = defaultGroundObjectsName;
			}
			else if(localCollider.gameObject.layer == wallLayer.value)
			{
				localCollider.gameObject.name = defaultWallObjectsName;
			}
		}
	}
}