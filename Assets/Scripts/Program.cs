using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Program : MonoBehaviour
{
    public static Program instance;
    public FlyCamera cameramovement;
    public bool enableCamera = false; //should be enabled by ui
    UIInputManager UImanager;
    SceneInputManager InputManager = new SceneInputManager();

    Program()
    {
        print("Loading program");
        instance = this;
    }

    private void Start()
    {
        UImanager = new UIInputManager(GetComponent<UIDocument>()); //initialize UIInputManager
        InputManager.EnableControls();
        cameramovement.enabled = false;
    }
    private void FixedUpdate()
    {
      if(InputManager.active == SceneInputManager.Modifier.Ctrl) //toggle Camera script
        {
            //Toggle CameraMovement Script
            cameramovement.enabled =! cameramovement.enabled;
            //toggle cursor
            UnityEngine.Cursor.visible = !cameramovement.enabled;
            Debug.Log("Toggle Camera" + cameramovement.enabled);
            InputManager.active = SceneInputManager.Modifier.none;


        }
    }
}
