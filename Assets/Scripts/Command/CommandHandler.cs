using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler
{	
	private List <ICommand> CommandList = new List<ICommand>();
	private int index;
	
	public void AddCommand(ICommand command)
	{
		if(index < CommandList.Count)
		CommandList.RemoveRange(index,CommandList.Count - index);
	    
		CommandList.Add(command);
		command.Execute();
		index++;
    }

    
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
    
	public void RedoCommand()
	{
		if(CommandList.Count == 0)
			return;
		if(index < CommandList.Count)
		{
			index++;
			CommandList[index - 1].Execute();
		}
	}
}
