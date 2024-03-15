using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBehaviour : MonoBehaviour
{
	[SerializeField] private LayerMask obstacleLayer;
	private const float boardSpacing = 1f;

	public void Move(Vector3 movement)
	{
		transform.position = transform.position + movement;
	}

	public bool IsValidMove(Vector3 movement)
	{
		return !Physics.Raycast(transform.position, movement, boardSpacing, obstacleLayer);
	}

	//void Start()
 //   {
	//    move = new IMove(this.transform,Vector3.up,0.01f);

 //       try 
	//    {
	//    	CommandHandler.instance.AddCommand(move);
	//    }
	    		
	//    catch (System.Exception e)
	//    {
	//    	//write E to Console
	//    	print("Missing Refrence to CommandHandler!");
	//    }
 //   }   

    void Update()
	{
		//if(Input.GetKey(KeyCode.W))
		//{
		//	move.SetDirection(Vector3.up);
		//	move.Execute();
		//}
		//if(Input.GetKey(KeyCode.A))
		//{
		//	move.SetDirection(Vector3.left);
		//	move.Execute();
		//}
		//if(Input.GetKey(KeyCode.S))
		//{
		//	move.SetDirection(Vector3.down);
		//	move.Execute();
		//}
		//if(Input.GetKey(KeyCode.D))
		//{
		//	move.SetDirection(Vector3.right);
		//	move.Execute();
		//}
		//if(Input.GetKey(KeyCode.Z))//&& Input.GetKey(KeyCode.LeftControl)
	 //   {
	 //   	move.Undo();
	 //   }
    }
}
