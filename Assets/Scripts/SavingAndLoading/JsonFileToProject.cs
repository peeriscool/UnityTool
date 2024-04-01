using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class JsonFileToProject
{
    public static ProjectData ProjectFile;

    public static void LoadData(ProjectData _data)
    {
        ProjectFile = _data;
      
        for (int i = 0; i < ProjectFile.SceneObjects.Count; i++) //load data from projectfile
        {
            ProjectFile.SceneObjects[i].make();
        }
    }
   
    public static void AddObject(GameObjectInScene obj)
    {
        
        if (ProjectFile.GetItemFromList(obj))
        {
            obj.Name = obj.Name + "_" + ProjectFile.SceneObjects.Count; //should add an increment to the name but adding a arbitrary value instead!   
            obj.setobjectname(obj.Name);
            Debug.Log("adding: " + obj.Name);
            ProjectFile.SceneObjects.Add(obj);
        }
        else
        {
            Debug.Log("adding: " + obj.Name);
            ProjectFile.SceneObjects.Add(obj);
        }
    }
    public static void CreateObjects()
    {
        for (int i = 0; i < ProjectFile.SceneObjects.Count; i++)
        {
            Debug.Log("Making: " +ProjectFile.SceneObjects[i].Name);
            ProjectData.AddobjList(ProjectFile.SceneObjects[i].make());
           /// SerializedObjects.Add(ProjectFile.SceneObjects[i].make());
        }
    }

}

//https://forum.unity.com/threads/save-gameobject-information-list-in-json-file.446615/
[System.Serializable]
public class GameObjectInScene
    {
    enum Owner { none,Root,Child,Subchild}
    public int ownerID;
   //public int InstanceId;
    private GameObject reference;
    public MeshSaveData Mymesh; //root
    /// public MeshSaveData[] Mymeshes; //additional meshes 
    public string Name;
    public Vector3 Scale;
    public Vector3 Position;
    public Quaternion Rotation;
    public GameObject Getrefrence()
    {
    return reference;
    }
    public GameObjectInScene(Transform childobject, int ownervalue)
    {
        GameObject obj = new GameObject();
        reference = obj;
        Name = childobject.name;
        Scale = childobject.localScale;
        Position = childobject.position;
        Rotation = childobject.rotation;
        Mymesh = new MeshSaveData(childobject.GetComponent<MeshFilter>().mesh); //adds empty meshfilter
        ownerID = ownervalue;
     //   InstanceId = obj.GetInstanceID();
    }
    /// <summary>
    /// adds an empty meshs
    /// </summary>
    public GameObjectInScene(string name, Vector3 scale, Vector3 position, Quaternion rotation)
    {
        GameObject obj = new GameObject();
        reference = obj;
        Name = name;
        Scale = scale;
        Position = position;
        Rotation = rotation;
        Mymesh = new MeshSaveData(obj.AddComponent<MeshFilter>().mesh); //adds empty meshfilter
    //    InstanceId = obj.GetInstanceID();
    }
    /// <summary>
    /// Try's to get mesh from filter
    /// </summary>
    public GameObjectInScene(GameObject obj)
    {
        reference = obj;
        Name = obj.name;
        Scale = obj.transform.localScale;
        Position = obj.transform.position;
        Rotation = obj.transform.rotation;
    //    InstanceId = obj.GetInstanceID();
        if (obj.TryGetComponent<MeshFilter>( out MeshFilter filter))
        {
            Mymesh = new MeshSaveData(filter.mesh);
        }
        else
        {
            Debug.Log("Warning Mesh Savedata is empty!");
            Mymesh = new MeshSaveData(obj.AddComponent<MeshFilter>().mesh); //adds empty mesh 
        }

    }
    public void setobjectname(string _name)
    {
        reference.name = _name;
    }

    public GameObject make() //make based on data in json
    {
        GameObject myobj = new GameObject();
        Mesh mesh = myobj.AddComponent<MeshFilter>().mesh;//myobj.AddComponent<MeshFilter>().mesh;
        
        myobj.name = Name;
        myobj.transform.localScale = Scale;
        myobj.transform.position = Position;
        myobj.transform.rotation = Rotation;
        
         
        if (!myobj.GetComponent<MeshRenderer>())
        {
            MeshRenderer renderer = myobj.AddComponent<MeshRenderer>();
            renderer.material = new Material(Shader.Find("Standard (Specular setup)")); //we should reload the material that was orginaly on the object
        }

        if (myobj.GetComponent<MeshFilter>() == null)
        {
            mesh = myobj.AddComponent<MeshFilter>().mesh;
        }
        else
        {
            mesh = myobj.GetComponent<MeshFilter>().mesh;
        }
        if(mesh.subMeshCount >= 0)
        {
            //we have submeshes
            
        }
        mesh.vertices = Mymesh.vertices;
        mesh.triangles = Mymesh.triangles;
        mesh.normals = Mymesh.normals;
        reference = myobj;
        if(ownerID == (int)Owner.Root)
        {
            RootObject(myobj.transform);
        }
        if (ownerID == (int)Owner.Child)
        {
            //we want to add this to a root object
            RootObject(myobj.transform);
        }
        //Debug.Log("_tri" + mesh.triangles.Length + "_vert" + mesh.vertices.Length + "_norm" + mesh.normals.Length);
        
        ExtensionMethods.AddMeshCollider(myobj);
        return myobj;
    }
    public void RootObject(Transform myobject) //add childs to root object creates root if necessary
    {
        if(ownerID == (int)Owner.Root)
        {
            Debug.Log("make root object" + Name);
            //GameObject root = new GameObject();
            myobject.gameObject.tag = "Root";
        }
        if(ownerID == (int)Owner.Child)
        {
                GameObject[] parents = GameObject.FindGameObjectsWithTag("Root");
                myobject.SetParent(parents[0].transform);
                Debug.Log("Assigning as child" + Name);
           
        }
    }
}

//https://discussions.unity.com/t/is-it-posible-to-save-mesh-to-json/255406/2
[System.Serializable]
public class MeshSaveData
{
    public int[] triangles;
    public Vector3[] vertices;  
    public Vector3[] normals;

    public MeshSaveData(Mesh mesh)
    {
        this.vertices = mesh.vertices;
        this.triangles = mesh.triangles;
        this.normals = mesh.normals;
    }
  
}

