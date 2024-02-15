using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBehaviour : MonoBehaviour
{
	IMove move;
	[SerializeField]
	public CommandHandler commandHandler = new CommandHandler();
	
    void Start()
    {
	    move = new IMove(this.transform,Vector3.up,0.01f);
	    
	    try 
	    {
	    	commandHandler.AddCommand(move);
	    }
	    		
	    catch (System.Exception e)
	    {
	    	//write E to Console
	    	print("Missing Refrence to CommandHandler!");
	    }
    }   

    void Update()
	{
		if(Input.GetKey(KeyCode.W))
		{
			move.SetDirection(Vector3.up);
			move.Execute();
		}
		if(Input.GetKey(KeyCode.A))
		{
			move.SetDirection(Vector3.left);
			move.Execute();
		}
		if(Input.GetKey(KeyCode.S))
		{
			move.SetDirection(Vector3.down);
			move.Execute();
		}
		if(Input.GetKey(KeyCode.D))
		{
			move.SetDirection(Vector3.right);
			move.Execute();
		}
		if(Input.GetKey(KeyCode.Z))//&& Input.GetKey(KeyCode.LeftControl)
	    {
	    	move.Undo();
	    }
    }
}
