using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnotherFileBrowser.Windows;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    print("Started UI manager");
        print("commandhandler = " + CommandHandler.instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void ButtonPress(string command)
	{
		//print("ActiveCommand = "+ command);
        //fix Make a dictonary so whe have a list of avalible commands 
        
        //Do requested Command
        if (command == "Redo")
        {
           print(CommandHandler.instance.RedoCommand());
            //if (CommandHandler.instance.RedoCommand() != true) {print("Redo"); };
        }
        if (command == "Undo")
		{
            print(CommandHandler.instance.UndoComand());
            //	if(CommandHandler.instance.UndoComand()!= true){print("Undo");};
        }
        if (command == "SavePrefab")
        {
            //  SavePrefab.Instance.SaveFunction(GameObject.Find("Player"));
            string path = openProjectFile();
            SavePrefab.Instance.ObjExportUtil(path);
        }
	}
    string openProjectFile()
    {
        string sendpath = "";
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load Binary or Json format of project
            Debug.Log(path);
            Debug.Log("Load Binary or Json format of project");
            sendpath = path;
           
        });
        return sendpath;
    }

    public void OnSliderValueChanged (float Value)
	{
        print(Value);
        CommandHandler.instance.IcommandHandler(new IMove(GameObject.Find("Player").transform, Vector3.left, Value));
    }
	
}
