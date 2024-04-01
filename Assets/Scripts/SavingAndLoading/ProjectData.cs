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
    public void SetDataFromRefrence(GameObject obj, Vector3 position )
    {
        bool sucses = false;
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if (SceneObjects[i].Getrefrence() == obj)
            {
                SceneObjects[i].Position = position;
                sucses = true;
                //object found set data to refrence
            }
            //for (int c = 0; c < SceneObjects[i].Getrefrence().transform.childCount; c++)
            //{
            //    if (SceneObjects[i].Getrefrence().transform.GetChild(c).gameObject.GetInstanceID() == obj.GetInstanceID())
            //    {
            //        SceneObjects[i].Getrefrence().transform.GetChild(c).gameObject.transform.position = position;
            //        sucses = true;
            //        //object found in child set data to refrence
            //    }
            //}
           
        }
        Debug.Log("Postion "+sucses);
    }
    public void SetDataFromRefrence(GameObject obj, Quaternion rotation)
    {
        bool sucses = false;

        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if (SceneObjects[i].Getrefrence() == obj)
            {
                SceneObjects[i].Rotation = rotation;
                sucses = true;
                //Debug.Log(SceneObjects[i].Position + " data/object" + rotation);

                //object found set data to refrence
            }
        }
        Debug.Log("rotation " + sucses);

    }
    public void SetDataFromRefrence(string objname, int scale)
    {
        bool sucses = false;

        for (int i = 0; i < SceneObjects.Count; i++)
        {
            if (SceneObjects[i].Name == objname)
            {
                SceneObjects[i].Scale = new Vector3(scale,scale,scale);
                Debug.Log(SceneObjects[i].Position + " data/object" + scale);
                sucses = true;
                //object found set data to refrence
            }
        }
        Debug.Log("Scale " + sucses);

    }
}