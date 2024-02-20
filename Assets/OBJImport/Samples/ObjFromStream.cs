using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ObjFromStream : MonoBehaviour {
	void Start () {
        UnityWebRequest www = new UnityWebRequest("https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj");
        //make www
       // var www = new WWW("https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj");
        while (!www.isDone)
            System.Threading.Thread.Sleep(1);
        
        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.url)); //www.text
        var loadedObj = new OBJLoader().Load(textStream);
	}
}
