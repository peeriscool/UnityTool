using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SavePrefab.Instance.SaveFunction(GameObject.Find("Player"));
        }
	}
	
	public void OnSliderValueChanged (float Value)
	{
        print(Value);
        CommandHandler.instance.IcommandHandler(new IMove(GameObject.Find("Player").transform, Vector3.left, Value));
    }
	
}
