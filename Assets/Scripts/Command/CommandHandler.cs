using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//https://unity.com/how-to/use-command-pattern-flexible-and-extensible-game-systems
public class CommandHandler
{
	public static CommandHandler instance = new CommandHandler();
	private static Stack<ICommand> _undoStack = new Stack<ICommand>();
	private static Stack<ICommand> _redoStack = new Stack<ICommand>();

    CommandHandler() 
	{
        instance = this;
    }

	public static void ExecuteCommand(ICommand command)
	{
		command.Execute();
		_undoStack.Push(command);

		// clear out the redo stack if we make a new move
		_redoStack.Clear();
	}
	/// <summary>
	/// Undo Command from command stack if not 0
	/// </summary>
	/// <returns>Bool</returns>
	public static void UndoComand()
    {		
	    if(_undoStack.Count > 0)
        {
			ICommand activecommand = _undoStack.Pop();
			_redoStack.Push(activecommand);
			activecommand.Undo();
        }
  
    }

	/// <summary>
	/// Redo Command from command stack if not 0
	/// </summary>
	/// <returns>Bool</returns>
	public static void RedoCommand()
	{
		if (_redoStack.Count > 0)
		{
			ICommand activeCommand = _redoStack.Pop();
			_undoStack.Push(activeCommand);
			activeCommand.Execute();
		}
	}
}
