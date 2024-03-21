﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using AnotherFileBrowser.Windows;
/// <summary>
/// TODO Rename To MENU InputManager
/// 
/// </summary>
public class UIInputManager
{
    private static UIInputManager Instance;
    public UIDocument Menu;
    Scene ProjectScene; //should be moved to different class
    Scene MenuScene; //should be moved to different class
    string ProjectName = "Empty";
    public UIInputManager(UIDocument Ui)
    {
        MenuScene = SceneManager.GetActiveScene();
        Menu = Ui;
        CreateUI();
        Instance = this;
        Debug.Log("Current Scene:"+MenuScene.name);
    }
    private void CreateUI() //creates the menu UI functions
    {
        VisualElement root = new VisualElement();
        root = Menu.rootVisualElement;//GetComponent<UIDocument>().rootVisualElement;
        root.visible = true;
        root.SetEnabled(true);
        Button Newbut = root.Q<Button>("New");
        Button Loadbut = root.Q<Button>("Load");
        Button Exitbut = root.Q<Button>("Exit");
        Button Controls = root.Q<Button>("Controls");
        Newbut.clicked += () => Startproject();
        Loadbut.clicked += () => openProjectFile();
        Exitbut.clicked += () => quit();
        //TODO Controls open and hide visual element ontop of menu
    }

    
    void Menuactive()
    {
        if (Menu.rootVisualElement.visible)
        {
            SceneManager.SetActiveScene(MenuScene);
            Debug.Log("Current Scene:" + MenuScene.name);
        }
        else if (ProjectScene != null)
        {
            SceneManager.SetActiveScene(ProjectScene);
            Debug.Log("Current Scene:" + ProjectScene.name);
        }
    }
    void openProjectFile()
    {

        var bp = new BrowserProperties();
        bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*"; //TODO: Implement Json
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
         {
             Debug.Log("Loading"+ bp +" Json project from" + path);
             //json.load iets
             ProjectData MyFile = JSONSerializer.Load<ProjectData>(path); //contains file name and extention
             JsonFileToProject.LoadData(MyFile);
         });
    }
    public void StartProjectFromJson(string _ProjectName)
    {
        ProjectName = _ProjectName;
        Startproject();
    }
    void Startproject()
    {
        
        if(ProjectName == "Empty") //If itss a new file populate with serializable objects
        {
            ProjectScene = SceneManager.CreateScene(ProjectName); //should use a file version number
            JsonFileToProject.ProjectFile = new ProjectData();
            JsonFileToProject.ProjectFile.ProjectName = ProjectName;
            JsonFileToProject.ProjectFile.Version = 0.1f;
            JsonFileToProject.ProjectFile.SceneObjects = new List<GameObjectInScene>();
            PopulateScene();
        }
        else //we already should have the data from Json
        {
            Debug.Log(ProjectName);
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name == ProjectName) //check if scene already exists
                if(SceneManager.GetSceneAt(i).name == ProjectName) //check if scene already exists
                {
                    //Can not create scene until old one is removed
                    SceneManager.UnloadSceneAsync(i);
                    ProjectScene = SceneManager.CreateScene(ProjectName); //should use a file version number
                }
               
            }
            if(ProjectName != null)
            {
                ProjectScene = SceneManager.CreateScene(ProjectName); //should use a file version number
            }
            CreateManager(); 
        }
        toggle();
        //make project active scene
        SceneManager.SetActiveScene(ProjectScene);
        Program.instance.cameramovement.enabled = true;  //Not a fan of Doing this here but ok
        UnityEngine.Cursor.visible = false;
	    // Debug.Log(Program.instance.cameramovement.isActiveAndEnabled);
    }
    void CreateManager()
    {
        GameObject commandmanager = new GameObject();
        commandmanager.name = "commandmanager";
        UIController controller = commandmanager.AddComponent<UIController>();
        controller.initialize();
        SceneManager.MoveGameObjectToScene(commandmanager, ProjectScene);
    }
    void PopulateScene() //runs when creating a new projectfile
    {
        //creating default objects in scene
        GameObjectInScene startcube = new GameObjectInScene(GameObject.CreatePrimitive(PrimitiveType.Cube));
        JsonFileToProject.AddObject(startcube);
        SceneManager.MoveGameObjectToScene(startcube.make(),ProjectScene);
          ///  SceneManager.MoveGameObjectToScene(GameObject.CreatePrimitive(PrimitiveType.Cube), ProjectScene)) ;
        CreateManager();

        GameObject baseplane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        baseplane.transform.localScale *= 10;
        baseplane.GetComponent<Collider>().enabled = false; //make ground planen non interactible
        //TODO  add grid material to baseplane
        //make sure plane does not get exported to obj
        SceneManager.MoveGameObjectToScene(baseplane, ProjectScene);

    }
    void quit()
    {
        //if editor 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //if apllication
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Warning nothing is saved yet!");

    }

    public static void toggleUi()
    { Instance.toggle(); Instance.Menuactive(); }

    void toggle()
    {
        Menu.rootVisualElement.visible = !Menu.rootVisualElement.visible;
    }
}
