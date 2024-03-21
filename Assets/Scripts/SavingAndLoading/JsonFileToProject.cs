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
        Program.instance.UImanager.StartProjectFromJson(ProjectFile.ProjectName); //create scene where we can add the content to handle some ui elements
       
        for (int i = 0; i < ProjectFile.SceneObjects.Count; i++) //load data from projectfile
        {
            Program.instance.AddobjectstoScene(ProjectFile.SceneObjects[i].make());
        }

    }
   
    public static void AddObject(GameObjectInScene obj)
    {
        Debug.Log("adding: " + obj.Name);
        ProjectFile.SceneObjects.Add(obj);
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

      public string Name;
      public Vector3 Scale;
      public Vector3 Position;
      public Quaternion Rotation;
        
        public GameObjectInScene(string name, Vector3 scale, Vector3 position, Quaternion rotation)
        {
            Name = name;
            Scale = scale;
            Position = position;
            Rotation = rotation;
        }
        public GameObjectInScene(GameObject obj)
        {
            Name = obj.name;
            Scale = obj.transform.localScale;
            Position = obj.transform.position;
            Rotation = obj.transform.rotation;
        }

        public GameObject make()
        {
            GameObject myobj = new GameObject();
            myobj.name = Name;
            myobj.transform.localScale = Scale;
            myobj.transform.position = Position;
            myobj.transform.rotation = Rotation;
            return myobj;
        }
    }

