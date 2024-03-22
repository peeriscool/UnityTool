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
    public string ProjectName;
    public float Version;
    public List<GameObjectInScene> SceneObjects;

    public ProjectData()
    {
        SceneObjects = new List<GameObjectInScene>();
        SerializedObjects = new List<GameObject>();  
    }
    [System.NonSerialized]
    public static List<GameObject> SerializedObjects;

    public static void AddobjList(GameObject _Obj)
    {
       
            Debug.Log("Nieuwe data:" + _Obj.name);
            GameObjectInScene obj = new GameObjectInScene(_Obj);
            JsonFileToProject.ProjectFile.SceneObjects.Add(obj);
            SerializedObjects.Add(_Obj);
    }
    public GameObject GetItemFromList(GameObjectInScene refrence)
    {
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if(SceneObjects[i].Name == refrence.Name)
            {
              return SceneObjects[i].Getrefrence();
            }
        }
        return null;
	    //for (int i = 0; i < SerializedObjects.Count; i++)
     //   {
		   // Debug.Log(i.ToString() + "out of : " + SerializedObjects.Count);
     //       if(SerializedObjects[i].name == refrence.Name)
     //       {
     //           return SerializedObjects[i];
     //       }
     //   }
     //   return null;
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