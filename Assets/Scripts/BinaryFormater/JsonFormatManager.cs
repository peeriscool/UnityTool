using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Runtime.Serialization;

//https://www.youtube.com/watch?v=5roZtuqZyuw binary
//https://medium.com/@defuncart/json-serialization-in-unity-9420abbce30b Json

public class JSONSerializer
{
    public static T Load<T>(string filename) where T : class
    {
        string path = Application.persistentDataPath + "/saves/" + filename + ".save";
        if (true) //JSONSerializer.PathExists(path)
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }
        return default(T);
    }

    public static void Save<T>(string filename, T data) where T : class
    {
        string path = Application.persistentDataPath + "/saves/" + filename + ".save";
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }
}

public class JsonFormatManager : MonoBehaviour
{
  public static bool Save(string saveName,object saveData)
    {
       
        //BinaryFormatter formatter = GetBinaryFormater();
        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

        FileStream file = File.Create(path);

       // formatter.Serialize(file, path);        
        file.Close();
        return true;
    }
    public static object Load(string path)
    {
        if (!File.Exists(path)) { return null; }
        //  BinaryFormatter 
        FileStream file = File.Open(path,FileMode.Open);

        try
        {
            //object save = GetBinaryFormater().Deserialize(file);
            file.Close();
            return null;
        }
        catch
        {
            Debug.LogErrorFormat("failed to load file at {0}", path);
            file.Close();
            return null;
        }
    }
    //public static BinaryFormatter GetBinaryFormater()
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    SurrogateSelector selector = new SurrogateSelector();
    //    quaternionSerializationSurrogate quaternionSurrgate = new quaternionSerializationSurrogate();
    //    vectorSerializationSurrogate vectorSurrgate = new vectorSerializationSurrogate();
    //    selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrgate);
    //    selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vectorSurrgate);
    //    formatter.SurrogateSelector = selector;
    //    return formatter;
    //}
}
