using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Runtime.Serialization;

//https://medium.com/@defuncart/json-serialization-in-unity-9420abbce30b Json
//https://code-maze.com/csharp-read-and-process-json-file/
//https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
/// <summary>
/// JSON Serialzer loads and Saves to Json
/// </summary>
public class JSONSerializer
{
    //overload when we know the path
    /// <summary>
    /// Load Objects from Json file
    /// </summary>
    /// <typeparam name="T">Class type that gets returned</typeparam>
    /// <param name="Path">Location of file to read</param>
    /// <returns></returns>
    public static T Load<T>(string Path) where T : class
    {
        if (true) //JSONSerializer.PathExists(path)
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(Path));
        }
#pragma warning disable CS0162 // Unreachable code detected
        return default(T);
#pragma warning restore CS0162 // Unreachable code detected
    }
    public static T Load<T>(string Path, string filename) where T : class
    {
        if (true) //JSONSerializer.PathExists(path)
        {
            return JsonUtility.FromJson<T>(Application.persistentDataPath + filename);
            //return JsonUtility.FromJson<T>(File.ReadAllText(Path));
        }
#pragma warning disable CS0162 // Unreachable code detected
        return default(T);
#pragma warning restore CS0162 // Unreachable code detected

    }

    /// <summary>
    /// Save serializable objects to Json file in persistent data path.
    /// </summary>
    /// <typeparam name="T">AnySaveable type</typeparam>
    /// <param name="filename">When not empty save to separate file</param>
    /// <param name="data">Actual data for serialization</param>
    public static void Save<T>(T data, string filename) where T : class
    {
        if (!string.IsNullOrEmpty(filename))
        {
            string path = Application.persistentDataPath + filename + ".Json"; //should create a folder persistentdatapath +/Folder/ + Filenmae + "Json"
            File.WriteAllText(path, JsonUtility.ToJson(data,true));
            Debug.Log("Saving Data to: " + Application.persistentDataPath + filename);
        }
    }
    /// <summary>
    /// Save serializable objects to Json file at given path.
    /// </summary>
    /// <typeparam name="T">AnySaveable type</typeparam>
    /// <param name="filename">When not empty save to separate file</param>
    /// <param name="data">Actual data for serialization</param>
    public static void Save<T>(string Path ,T data) where T : class
    {
        if(!string.IsNullOrEmpty(Path))
        {
            //if given path is not empty or null, Write data to location
            File.WriteAllText(Path, JsonUtility.ToJson(data,true));
            Debug.Log("Saving Data to: " +Path);
        }
        else
        {
            //create a pop up that lets the user know that the object could not be saved 
        }
    }

}

