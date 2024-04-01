using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


/// <summary>
/// Program:
/// , Camera Control
/// , UIInputManger /startmenuHandler
/// , InputManager
/// , SelectionManager
/// 
/// </summary>
[RequireComponent(typeof(UIDocument))]
public class Program : MonoBehaviour
{
    public static Program instance;
    public FlyCamera camcontroler;
    public bool enableCamera = false; //should be enabled by ui
    public UiManager Uimanager;
    [SerializeField]
    InputManager InputManager = new InputManager();
    SelectionManager Selectionmanager = new SelectionManager();
    Program()
    {
        print("Loading program Setting Instance...");
        instance = this;
    }

    private void Start()
    {
        Uimanager = new UiManager(GetComponent<UIDocument>()); //initialize UIInputManager
        Uimanager.StartApp();
        InputManager.EnableControls();
        camcontroler.enabled = false; //Disable Camera Movement when program starts
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name != "StartMenu")
        {
            Selectionmanager.UpdateInput();
        }
    }

}
