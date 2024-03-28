using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//https://unity.com/how-to/use-command-pattern-flexible-and-extensible-game-systems
// https://www.youtube.com/watch?v=attURV3JWKQ

/// <summary>
/// invoker class calls execute and undo using a stack of Icommands.
/// stack represents all commands done during session
/// TODO 
/// - Should comunicate with Json so we can save histroy
/// - Implement functionality
/// </summary>
public class CommandInvoker
{
	public static CommandInvoker instance = new CommandInvoker();
	private static Stack<ICommand> _undoStack = new Stack<ICommand>();
	private static Stack<ICommand> _redoStack = new Stack<ICommand>();

	CommandInvoker() 
	{
        instance = this;
    }
	//public static void AddCommand(ICommand command)
	//{
	//	_undoStack.Push(command);
	//}
	public static void ExecuteCommand(ICommand command)
	{
		Debug.Log("Execute and adding to stack, " + command);
		command.Execute();
		_undoStack.Push(command);
		// clear out the redo stack if we make a new move
		_redoStack.Clear();
	}
	public static void UndoComand()
    {		
	    if(_undoStack.Count > 0)
        {
			ICommand activecommand = _undoStack.Pop();
			Debug.Log("Undoing: " + activecommand);
			_redoStack.Push(activecommand);
			activecommand.Undo();
        }
        else
        {
			Debug.Log(_undoStack.Count);
        }
    }
	public static void RedoCommand()
	{
		if (_redoStack.Count > 0)
		{
			ICommand activeCommand = _redoStack.Pop();
			_undoStack.Push(activeCommand);
			activeCommand.Execute();
		}
		else
		{
			Debug.Log(_undoStack.Count);
		}
	}
}
