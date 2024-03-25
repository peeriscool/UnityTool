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
            Debug.Log("adding:objects");
            ProjectFile.SceneObjects[i].make();
        }

    }
   
    public static void AddObject(GameObjectInScene obj)
    {
        if(ProjectFile.SceneObjects.Contains(obj))
        {
            obj.Name = obj.Name +"_"+ ProjectFile.SceneObjects.Count; //should add an increment to the name but adding a arbitrary value instead!   
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
            ProjectData.AddobjList(ProjectFile.SceneObjects[i].make());
           /// SerializedObjects.Add(ProjectFile.SceneObjects[i].make());
        }
    }

}



//https://forum.unity.com/threads/save-gameobject-information-list-in-json-file.446615/
[System.Serializable]
public class GameObjectInScene
    {
      private GameObject reference;
      public MeshSaveData Mymesh;  
      public string Name;
      public Vector3 Scale;
      public Vector3 Position;
      public Quaternion Rotation;
        public GameObject Getrefrence()
        {
        return reference;
        }
        public GameObjectInScene(string name, Vector3 scale, Vector3 position, Quaternion rotation)
        {
            GameObject obj = new GameObject();
            reference = obj;
            Name = name;
            Scale = scale;
            Position = position;
            Rotation = rotation;
            Mymesh = new MeshSaveData(obj.AddComponent<MeshFilter>().mesh);

        }
        //ToDO: Make overload that sets childerenMeshes of imported objects
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="filter"></param>
        public GameObjectInScene(GameObject obj,MeshFilter filter)
        {
            reference = obj;
            Name = obj.name;
            Scale = obj.transform.localScale;
            Position = obj.transform.position;
            Rotation = obj.transform.rotation;
            Mymesh = new MeshSaveData(filter.mesh);
        }
        public GameObjectInScene(GameObject obj)
        {
            reference = obj;
            Name = obj.name;
            Scale = obj.transform.localScale;
            Position = obj.transform.position;
            Rotation = obj.transform.rotation;
            if(obj.TryGetComponent<MeshFilter>( out MeshFilter filter))
            {
                Mymesh = new MeshSaveData(filter.mesh);
            }
            else
            {
            Mymesh = new MeshSaveData(obj.AddComponent<MeshFilter>().mesh);
            }
        }

        public GameObject make()
        {
            GameObject myobj = new GameObject();
            myobj.name = Name;
            myobj.transform.localScale = Scale;
            myobj.transform.position = Position;
            myobj.transform.rotation = Rotation;
            if(!myobj.GetComponent<MeshRenderer>())
             {
                MeshRenderer renderer = myobj.AddComponent<MeshRenderer>();
                renderer.material = new Material(Shader.Find("Standard (Specular setup)"));
             }
            if(GameObject.Find("commandmanager"))
            {
                GameObject.Find("commandmanager").GetComponent<UIController>().GenerateBoxcolliderOnMesh(myobj, myobj.AddComponent<MeshFilter>());

            }
            else
            {
                myobj.AddComponent<MeshFilter>();
            }
            Mesh mesh = myobj.GetComponent<MeshFilter>().mesh;
            mesh.vertices = Mymesh.vertices;
            mesh.triangles = Mymesh.triangles;
            mesh.normals = Mymesh.normals;
            reference = myobj;
            return myobj;
        }
    }

//https://discussions.unity.com/t/is-it-posible-to-save-mesh-to-json/255406/2
[System.Serializable]
public class MeshSaveData
{
    public int[] triangles;
    public Vector3[] vertices;  
    public Vector3[] normals;

    // add whatever properties of the mesh you need...

    public MeshSaveData(Mesh mesh)
    {
        this.vertices = mesh.vertices;
        this.triangles = mesh.triangles;
        this.normals = mesh.normals;
        // further properties...
    }
}

