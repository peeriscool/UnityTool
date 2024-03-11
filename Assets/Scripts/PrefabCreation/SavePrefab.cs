using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SavePrefab : MonoBehaviour
{
	public static SavePrefab Instance { get; private set; }  //singelton refrence
	public bool Savebool = false;
	void Awake()
	{
		// If there is an instance, and it's not me, delete myself.

		if (Instance != null && Instance != this)
		{
			Destroy(this); //monobehaviour dependicy
		}
		else
		{
			Instance = this;
		}
	}
	/// <summary>
	/// Creates a Prefab in the editor
	/// </summary>
	/// <param name="obj">Object to save as prefab</param>
	public void SaveFunction(GameObject obj) //Unity editor dependicy
	{
#if UNITY_EDITOR

		string localPath = "Assets/Prefabs/" + obj.name + ".prefab";

		//make file name unique if name already exists
		localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

		//Create  prefab
		PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction);
#endif

	}
	//Callable function for saving object during runtime 
	public void ObjExportUtil(string _path)
	{
		OBJExporter exportutil = new OBJExporter();
		exportutil.Export(_path);

	}
}