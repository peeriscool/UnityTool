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
    public FlyCamera camcontroler;
    public bool enableCamera = false; //should be enabled by ui
    public UIManager UImanager;
    InputManager InputManager = new InputManager();
    Program()
    {
        print("Loading program Setting Instance...");
        instance = this;
      
    }

    private void Start()
    {
        UImanager = new UIManager(GetComponent<UIDocument>()); //initialize UIInputManager
        InputManager.EnableControls();
        camcontroler.enabled = false; //Disable Camera Movement when program starts
    }
   
}
