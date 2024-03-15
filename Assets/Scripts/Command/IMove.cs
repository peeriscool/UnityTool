using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class IMove : ICommand
{
	MoveableBehaviour playerMover;
	Vector3 movement;
	public IMove(MoveableBehaviour player, Vector3 moveVector)
	{
		this.playerMover = player;
		this.movement = moveVector;
	}
	public void Execute()
	{
		playerMover.Move(movement);
		Debug.Log(playerMover.transform.position);
	}
	public void Undo()
	{
		playerMover.Move(-movement);
	}
	//[SerializeField]
	//private Vector3 direction = Vector3.zero;
	//private float distance = 1f;
	//private Transform objectToMove;

	//public IMove(Transform objectToMove, Vector3 direction, float distance)
	//{
	//	this.direction = direction;
	//	this.objectToMove = objectToMove;
	//	this.distance = distance;
	//}

	//public void Execute()
	//{		
	//	objectToMove.position += direction * distance;
	//}

	//public void Undo()
	//{
	//	objectToMove.position -= direction * distance;
	//}
	//public void SetDirection(Vector3 direction)
	//{
	//	this.direction = direction;
	//}
	//public void Setdistance(float distance)
	//{
	//	this.distance = distance;
	//}
	////used to draw the path of the object
	//public Vector3 GetMove()
	//{
	//	return direction * distance;
	//}
}
