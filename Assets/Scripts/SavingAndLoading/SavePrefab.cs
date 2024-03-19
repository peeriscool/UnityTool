using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class SavePrefab
{
	/// <summary>
	/// Creates a Prefab in the editor
	/// </summary>
	/// <param name="obj">Object to save as prefab</param>
	public static void Save(GameObject obj) //Unity editor dependicy
	{
#if UNITY_EDITOR

		string localPath = "Assets/Prefabs/" + obj.name + ".prefab";

		//make file name unique if name already exists
		localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

		//Create  prefab
		PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction);
#endif

	}
	//editor function for saving object 
	public static void ObjExportUtil(string _path,List<GameObject> objs)
	{
		ObjExporterStandalone exportUtil = new ObjExporterStandalone();
		//OBJExporter exportutil = new OBJExporter();
		//exportutil.Export(_path); //ExportRutime(_path);
		exportUtil.Export(_path, objs);
	}


}