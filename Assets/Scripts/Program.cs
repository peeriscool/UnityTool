using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


/// <summary>
/// Program manages:
/// - Camera Control
/// - UIInputManger
/// - SceneInputManager
/// 
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class Program : MonoBehaviour
{
    public static Program instance;
    public FlyCamera cameramovement;
    public bool enableCamera = false; //should be enabled by ui
    public UIInputManager UImanager;
    SceneInputManager InputManager = new SceneInputManager();
    Program()
    {
        print("Loading program Setting Instance...");
        instance = this;
    }

    private void Start()
    {
        UImanager = new UIInputManager(GetComponent<UIDocument>()); //initialize UIInputManager
        InputManager.EnableControls();
        cameramovement.enabled = false; //Disable Camera Movement when program starts
    }
    private void FixedUpdate()
    {
      if(InputManager.active == SceneInputManager.Modifier.Ctrl) //toggle Camera script, Should be done In SceneInputmanager!
        {
            //Toggle CameraMovement Script
            cameramovement.enabled =! cameramovement.enabled;
            //toggle cursor
            UnityEngine.Cursor.visible = !cameramovement.enabled;
           // Debug.Log("Toggle Camera" + cameramovement.enabled);
            InputManager.active = SceneInputManager.Modifier.none;
        }
    }
    public void AddobjectstoScene(GameObject obj)
    {
        GameObject.Instantiate(obj);
    }
}
