using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Runtime.Serialization;

//https://medium.com/@defuncart/json-serialization-in-unity-9420abbce30b Json
//https://code-maze.com/csharp-read-and-process-json-file/
public class JSONSerializer
{
    //overload when we know the path
    public static T Load<T>(string Path) where T : class
    {
        if (true) //JSONSerializer.PathExists(path)
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(Path));
        }
        return default(T);
    }

    public static void Save<T>(string filename, T data) where T : class
    {
        string path = Application.persistentDataPath + filename + ".Json"; //should create a folder persistentdatapath +/Folder/ + Filenmae + "Json"
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }
    public static void Save<T>(string filename,string Path ,T data) where T : class
    {
        if(!string.IsNullOrEmpty(Path))
        {
            //if not empty or null
            File.WriteAllText(Path, JsonUtility.ToJson(data));

        }
        else
        {
            //create a pop up that lets the user know that the object could not be saved 
        }
    }

}
[System.Serializable]
public class ProjectData
{
    public string ProjectName;
    public float Version;
    [System.NonSerialized] private int Value = 0;
    private int somePrivateVariable = 1;
}
