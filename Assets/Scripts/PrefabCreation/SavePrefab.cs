using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SavePrefab : MonoBehaviour
{
	public static SavePrefab Instance { get; private set; }
	public bool Savebool = false;
	// Start is called before the first frame update
	void Awake()
	{
		// If there is an instance, and it's not me, delete myself.

		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}
	public void SaveFunction(GameObject obj)
    {
		#if UNITY_EDITOR

		string localPath = "Assets/Prefabs/" + obj.name + ".prefab";
	    
	    //make file name unique if name already exists
	    localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
	    
	    //Create  prefab
	    PrefabUtility.SaveAsPrefabAssetAndConnect(obj,localPath,InteractionMode.UserAction);
    	#endif
    }
	public void ObjExportUtil(string _path)
    {
		OBJExporter exportutil = new OBJExporter();
		exportutil.Export(_path);

	}

 //   // Update is called once per frame
	//void Update()
 //   {
	//    if(Savebool)
	//    {
	//    	SaveFunction(this.gameObject);
	//    	Savebool = false;
	//    }
 //   }
}
