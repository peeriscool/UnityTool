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
}
