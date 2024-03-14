using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


class SceneInputManager : MonoBehaviour
{
    Actions Playeractions;
    enum Modifier { none, Ctrl, alt, shift }
    Modifier active;
    void Awake()
    {
        active = Modifier.none;
        Playeractions = new Actions();
        Playeractions.InputMapping.Letters.Enable();
        Playeractions.InputMapping.Menucontrolls.Enable();
        Playeractions.InputMapping.Letters.performed += Letters_performed;
        Playeractions.InputMapping.Menucontrolls.started += Menucontrolls_started;
        Playeractions.InputMapping.Menucontrolls.canceled += Menucontrolls_canceled;

    }

    private void Menucontrolls_canceled(InputAction.CallbackContext obj)
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
        }
        //If ctrl Redo
        if (context.action.activeControl == Keyboard.current.yKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
        }
        //If ctrl Paste
        if (context.action.activeControl == Keyboard.current.vKey && active == Modifier.Ctrl)
        {
            Debug.Log(context.action.activeControl.name);
        }

    }
}
  