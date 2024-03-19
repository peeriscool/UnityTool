using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


class SceneInputManager
{
    Actions Playeractions;
    public enum Modifier { none, Ctrl, alt, shift }
    public Modifier active;

    public void EnableControls()
    {
        active = Modifier.none;
        Playeractions = new Actions();
        Playeractions.InputMapping.Letters.Enable();
        Playeractions.InputMapping.Menucontrolls.Enable();
        Playeractions.InputMapping.Mouse.Enable();
        Playeractions.InputMapping.Mouse.started += Mouse_started;
        Playeractions.InputMapping.Mouse.performed += Mouse_context;
        Playeractions.InputMapping.Mouse.canceled += Mouse_context;
        Playeractions.InputMapping.Letters.performed += Letters_performed;
        Playeractions.InputMapping.Menucontrolls.started += Menucontrolls_started;
        Playeractions.InputMapping.Menucontrolls.canceled += Menucontrolls_canceled;
        Playeractions.InputMapping.Menucontrolls.performed += Menucontrolls_performed;
    }

    private void Mouse_started(InputAction.CallbackContext context) //seems to be prettymuch overwriten by performed
    {
        //if (context.started) //fires once
        //{
        //    Debug.Log("Started");
        //    if (context.action.activeControl == Mouse.current.leftButton)
        //    {
        //        SelectionManager.instance.selection = SelectionManager.state.pickup;
        //    }
        //}
    }

    //change State of the mouse to Pickup,hold or Release
    private void Mouse_context(InputAction.CallbackContext context)
    {
      
     
       if(context.performed) //pressing down
        {
            if (context.action.activeControl == Mouse.current.leftButton && active == Modifier.shift) //multiselect
            {
                SelectionManager.instance.selection = SelectionManager.state.hold;
            }
            if (context.action.activeControl == Mouse.current.leftButton) 
            {
                SelectionManager.instance.selection = SelectionManager.state.hold;                
            }
       }

        if (context.canceled) //released
        {
            if (context.action.activeControl == Mouse.current.leftButton) 
            {
                SelectionManager.instance.selection = SelectionManager.state.release;
            }
        }
    }

    //esc to toggle ui
    private void Menucontrolls_performed(InputAction.CallbackContext context)
    {
        if (context.action.activeControl == Keyboard.current.escapeKey)
        {
            UIInputManager.toggleUi();
        }
        //hold functions
        //if (context.action.activeControl == Keyboard.current.enterKey)
        //{
        //    //DO confirm
        //}

        //if (context.action.activeControl == Keyboard.current.leftShiftKey)
        //{
        //    //modifier
        //}

        //if (context.action.activeControl == Keyboard.current.leftCtrlKey)
        //{
        //    //modifier
        //}
    }

    private void Menucontrolls_canceled(InputAction.CallbackContext context)
    {
    //    Debug.Log("null");
        active = 0;
    }

    private void Menucontrolls_started(InputAction.CallbackContext context)
    {
        if (context.action.activeControl == Keyboard.current.leftCtrlKey)
        {
            active = Modifier.Ctrl;
        }
        else if (context.action.activeControl == Keyboard.current.leftAltKey)
        {
            active = Modifier.alt;
        }
        else if(context.action.activeControl == Keyboard.current.leftShiftKey)
        {
            active = Modifier.shift;
        }
        else
        {
            active = Modifier.none;
        }
    }

    private void Letters_performed(InputAction.CallbackContext context)
    {
        //If ctrl select all
        if (context.action.activeControl == Keyboard.current.aKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
        }
        //If ctrl Save
        if (context.action.activeControl == Keyboard.current.sKey && active == Modifier.Ctrl)
        {
            Debug.Log(active.ToString());
        }
        //If ctrl Cut
        if (context.action.activeControl == Keyboard.current.xKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
        }
        //If ctrl Undo
        if (context.action.activeControl == Keyboard.current.zKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
            CommandHandler.UndoComand();
        }
        //If ctrl Redo
        if (context.action.activeControl == Keyboard.current.yKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
            CommandHandler.RedoCommand();
        }
        //If ctrl Paste
        if (context.action.activeControl == Keyboard.current.vKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
           
        }

    }
}
  