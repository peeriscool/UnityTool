using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Actual data that is being writen to the JsonFIle
/// </summary>
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
    }
    public void SetDataFromRefrence(string objname, Vector3 position )
    {
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if (SceneObjects[i].Name == objname)
            {
                SceneObjects[i].Position = position;
                Debug.Log(SceneObjects[i].Position + " data/object" +position);

                //object found set data to refrence
            }
        }
    }
    public void SetDataFromRefrence(string objname, Quaternion rotation)
    {
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if (SceneObjects[i].Name == objname)
            {
                SceneObjects[i].Rotation = rotation;
                Debug.Log(SceneObjects[i].Position + " data/object" + rotation);

                //object found set data to refrence
            }
        }
    }
    public void SetDataFromRefrence(string objname, int scale)
    {
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if (SceneObjects[i].Name == objname)
            {
                SceneObjects[i].Scale = new Vector3(scale,scale,scale);
                Debug.Log(SceneObjects[i].Position + " data/object" + scale);

                //object found set data to refrence
            }
        }
    }
}