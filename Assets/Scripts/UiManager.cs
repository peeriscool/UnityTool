using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    print("Started UI manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void ButtonPress(string command)
	{
		print("StartButtonPress"+ command);
		//fix this by making commandhandler singleton
		//find commandhandler for move
		CommandHandler CHref = GameObject.FindObjectOfType<MoveableBehaviour>().commandHandler;
		//Do requested Command
		if(command == "UndoMove")
		{
			if(CHref.UndoComand()!= true){print("EmptyUndo");};
		}
	}
	
	public void test()
	{}
	
}
