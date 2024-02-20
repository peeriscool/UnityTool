using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CommandHandler
{
	public static CommandHandler instance = new CommandHandler();	
	private List <ICommand> CommandList = new List<ICommand>();
	private int index;
    CommandHandler() 
	{
        instance = this;
    }
    public void AddCommand(ICommand command)
	{
		if(index < CommandList.Count)
		CommandList.RemoveRange(index,CommandList.Count - index);
	    
		CommandList.Add(command);
		command.Execute();
		index++;
    }

    /// <summary>
    /// Undo Command from commandlist if possible
    /// </summary>
    /// <returns>Bool</returns>
    public bool UndoComand()
    {		
	    if(CommandList.Count == 0)
		    return false;
	    if (index > 0)
	    {
	    	CommandList[index -1].Undo();
		    index--;   
	    }
	    return true;
    }
    
	/// <summary>
	/// Redo Command from commandlist if possible
	/// </summary>
	/// <returns>Bool</returns>
	public bool RedoCommand()
	{
		if(CommandList.Count == 0) return false;
		if(index < CommandList.Count)
		{
			index++;
			CommandList[index - 1].Execute();
		}
		return true;
	}

	public void IcommandHandler(ICommand com)
	{
		if(com.GetType() == typeof(IMove))
		{
			//
			CommandList.Add(com);
            com.Execute();
		}
	}
}
