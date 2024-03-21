using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ProjectData
{
   public ProjectData()
    {
        SceneObjects = new List<GameObjectInScene>();
        SerializedObjects = new List<GameObject>();  
    }
    public string ProjectName;
    public float Version;
    public List<GameObjectInScene> SceneObjects;
  
    [System.NonSerialized]
    public static List<GameObject> SerializedObjects;

    public static void AddobjList(GameObject _Obj)
    {
       
            Debug.Log("Nieuwe data:" + _Obj.name);
            GameObjectInScene obj = new GameObjectInScene(_Obj);
            JsonFileToProject.ProjectFile.SceneObjects.Add(obj);
    }
}


//if(SerializedObjects != null)
//{
//    SerializedObjects.Add(_Obj);
//}
//else
//{
//    SerializedObjects = new List<GameObject>();
//    SerializedObjects.Add(_Obj);
//    GameObjectInScene obj = new GameObjectInScene(_Obj);

//    SceneObjects.Add(obj);
//}