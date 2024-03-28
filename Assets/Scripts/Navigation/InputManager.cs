using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
class InputManager
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
        Playeractions.InputMapping.Mouse.performed += Mouse_context;
        Playeractions.InputMapping.Mouse.canceled += Mouse_context;
        Playeractions.InputMapping.Letters.performed += Letters_performed;
        Playeractions.InputMapping.Menucontrolls.started += Menucontrolls_started;
        Playeractions.InputMapping.Menucontrolls.canceled += Menucontrolls_canceled;
        Playeractions.InputMapping.Menucontrolls.performed += Menucontrolls_performed;
    }

    //change State of the mouse to Pickup,hold or Release
    private void Mouse_context(InputAction.CallbackContext context)
    {
       if(context.performed) //pressing down
        {
            if (context.action.activeControl == Mouse.current.leftButton && active == Modifier.shift) //TODO multiselect
            {
                SelectionManager.instance.selection = SelectionManager.state.hold;
            }
            if (context.action.activeControl == Mouse.current.leftButton) 
            {
                SelectionManager.instance.selection = SelectionManager.state.hold;                
            }
            if (context.action.activeControl == Mouse.current.rightButton)
            {
                SelectionManager.instance.selection = SelectionManager.state.Right;
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
            switch (Program.instance.Uimanager.activeUI)
            {
                case "StartMenu":
                    Program.instance.Uimanager.CallPalleteHandler();
                    break;
                case "Pallete": Program.instance.Uimanager.StartMenuHandler();
                    break;
                default: ;
                    if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name !=Program.instance.gameObject.scene.name) //check if we have a project scene to go back to, if not don't toggle UiMenu
                    {
                        Debug.Log("Esc cant perform ActiveUI is Startmenu go back to pallete");
                     Program.instance.Uimanager.CallPalleteHandler();
                    }
                  break;
            }
        }      
    }

    private void Menucontrolls_canceled(InputAction.CallbackContext context)
    {
        active = Modifier.none;
    }

    private void Menucontrolls_started(InputAction.CallbackContext context)
    {
        if (context.action.activeControl == Keyboard.current.leftCtrlKey)
        {
            active = Modifier.Ctrl;
            ToggleCamera();

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
          //  Debug.Log("ctrl " + context.action.activeControl.name);
            CommandInvoker.UndoComand();
        }
        //If ctrl Redo
        if (context.action.activeControl == Keyboard.current.yKey && active == Modifier.Ctrl)
        {
         //   Debug.Log("ctrl " + context.action.activeControl.name);
            CommandInvoker.RedoCommand();
        }
        //If ctrl Paste
        if (context.action.activeControl == Keyboard.current.vKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
           
        }

    }

    public void ToggleCamera()
    {
        if (active == Modifier.Ctrl) //toggle Camera script, Should be done In Inputmanager!
        {
            //Toggle CameraMovement Script
            FlyCamera.Instance.enabled = !FlyCamera.Instance.enabled;
            //toggle cursor
            UnityEngine.Cursor.visible = !FlyCamera.Instance.enabled;
             Debug.Log("Toggle Camera" + FlyCamera.Instance.enabled);
           // active = Modifier.none;
        }
    }
}

  