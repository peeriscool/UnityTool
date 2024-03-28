using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
static class ExtensionMethods
{
	/// <summary>
	/// Rounds Vector3.
	/// </summary>
	/// <param name="vector3"></param>
	/// <param name="decimalPlaces"></param>
	/// <returns></returns>
	public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
	{
		float multiplier = 1;
		for (int i = 0; i < decimalPlaces; i++)
		{
			multiplier *= 10f;
		}
		return new Vector3(
			Mathf.Round(vector3.x * multiplier) / multiplier,
			Mathf.Round(vector3.y * multiplier) / multiplier,
			Mathf.Round(vector3.z * multiplier) / multiplier);
	}
	/// <summary>
	/// Makes a Plane with a grid texture from resources into a specific scene
	/// </summary>
	/// <param name="ProjectScene"></param>
	public static void makebaseplane(Scene ProjectScene)
	{
		//TODO make sure plane does not get exported to obj
		GameObject baseplane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		baseplane.transform.localScale *= 4;
		baseplane.name = "BasePlane";
		baseplane.GetComponent<Collider>().enabled = false; //make ground planen non interactible
		baseplane.GetComponent<Renderer>().material = Resources.Load<Material>("GridMaterial");
		SceneManager.MoveGameObjectToScene(baseplane, ProjectScene);
	}
	/// <summary>
	/// /Adds a cube to the startScene
	/// </summary>
	/// <param name="ProjectScene"></param>
	public static void MakeStartCube(Scene ProjectScene) //runs when creating a new projectfile
	{
		//creating default objects in scene
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		GameObjectInScene startcube = new GameObjectInScene(cube);
		JsonFileToProject.AddObject(startcube); //make it saveable
		SceneManager.MoveGameObjectToScene(JsonFileToProject.ProjectFile.GetItemFromList(startcube), ProjectScene);
	}
	public static void CreateManager(Scene ProjectScene)
	{
		GameObject commandmanager = new GameObject();
		commandmanager.name = "commandmanager";
		UIController controller = commandmanager.AddComponent<UIController>();
		//controller.initialize();
		SceneManager.MoveGameObjectToScene(commandmanager, ProjectScene);
	}

    //https://gist.github.com/danielbierwirth/4704573841072d4646f950685fb86c04
    public static void AddMeshCollider(GameObject containerModel)
    {
        // Add mesh collider
        MeshFilter meshFilter = containerModel.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            MeshCollider meshCollider = containerModel.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = meshFilter.sharedMesh;
        }
        // Add mesh collider (convex) for each mesh in child elements.
        Component[] meshes = containerModel.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mesh in meshes)
        {
            MeshCollider meshCollider = containerModel.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh.sharedMesh;
        }
    }
    /// <summary>
    /// NOT OPTIMIZED!!! Fixes the colliders when pivot point is not on mesh location
    /// </summary>
    /// <param name="ImportedObject"></param>
    public static void GenerateBoxcolliderOnMesh(GameObject ImportedObject, MeshFilter filter)
    {
        if (ImportedObject != null)
        {
            BoxCollider Mycollider = ImportedObject.AddComponent<BoxCollider>(); //make it so we can select it with the selectionmanager
            int Submeshcount = ImportedObject.transform.childCount;
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;
            Vector3 center = new Vector3();
            //used if we need the childeren to calculate the colliders
            List<Vector3> allmin = new List<Vector3>();
            List<Vector3> allmax = new List<Vector3>();

            if (filter.mesh.vertices.Length > 0) //use root to calculate boxcolider
            {
                Debug.Log("Running through" + " meshFilter for center");
                for (int i = 0; i < filter.mesh.vertices.Length; i++)
                {
                    min = Vector3.Min(filter.mesh.vertices[i], min);
                    max = Vector3.Max(filter.mesh.vertices[i], max);
                    center += filter.mesh.vertices[i];
                }
                center.x = center.x / filter.mesh.vertices.Length;
                center.y = center.y / filter.mesh.vertices.Length;
                center.z = center.z / filter.mesh.vertices.Length;
            }
            else if (filter.mesh.vertices.Length == 0 && Submeshcount != 0) //root object does not have any mesh data
            {
                Debug.Log("Running through" + Submeshcount + " child meshes");
                //generate collider location on childeren
                for (int i = 0; i < Submeshcount; i++)
                {
                    Transform child = ImportedObject.transform.GetChild(i);
                    Mesh childmesh = child.GetComponent<MeshFilter>().mesh;

                    if (childmesh != null)
                    {
                        Debug.Log("Running through" + childmesh.vertices.Length + " vertices");
                        for (int j = 0; j < childmesh.vertices.Length; j++)
                        {
                            //   Debug.Log(childmesh.vertices[j]);
                            min = Vector3.Min(childmesh.vertices[j], min);
                            max = Vector3.Max(childmesh.vertices[j], max);
                            allmin.Add(min);
                            allmax.Add(max);
                            center += childmesh.vertices[j];
                        }
                    }
                    for (int x = 1; x < allmax.Count; x++)
                    {
                        max = Vector3.Max(allmax[x], allmax[x - 1]);
                    }
                    //   center.x = center.x / childmesh.vertices.Length;
                    //  center.y = center.y / childmesh.vertices.Length;
                    //   center.z = center.z / childmesh.vertices.Length;
                    center = Vector3.zero;
                    Debug.Log(childmesh.name + " minimal " + min + "root = " + ImportedObject.name);
                    Debug.Log(childmesh.name + " maximum " + max + "root = " + ImportedObject.name);
                }
            }
            else //meshfilter is empty
            {
                max = Vector3.one;
            }
            Vector3 size = max;
            size.x = size.z;
            Mycollider.size = size; //X value seems to be 0
            Mycollider.center = center;
            Debug.Log("center " + center);
        }
    }
}