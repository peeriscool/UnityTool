using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using AnotherFileBrowser.Windows;
/// <summary>
/// TODO remove code that is not UI related Like Mouse cursor toggles and scene loading
/// 
/// </summary>
public class UIManager
{
    private static UIManager Instance;
    public UIDocument Menu;
    Scene ProjectScene; //should be moved to Project data
    Scene MenuScene; //should be moved to Project data
    string ProjectName = "Empty";
    VisualElement root = new VisualElement(); //root of menu
    VisualElement controlmenu; //displays controls to user

    public UIManager(UIDocument Ui)
    {
        MenuScene = SceneManager.GetActiveScene();
        Menu = Ui;
        CreateUI();
        Instance = this;
        Debug.Log("Current Scene:"+MenuScene.name);
    }
    private void CreateUI() //creates the menu UI functions
    {
        root = Menu.rootVisualElement;//GetComponent<UIDocument>().rootVisualElement;
        root.visible = true;
        root.SetEnabled(true);
        Button Newbut = root.Q<Button>("New");
        Button Loadbut = root.Q<Button>("Load");
        Button Exitbut = root.Q<Button>("Exit");
        Button Controls = root.Q<Button>("Control");

        controlmenu = root.Q<VisualElement>("ControlsMenu");
        controlmenu.visible = false;

        Newbut.clicked += () => Startproject();
        Loadbut.clicked += () => openProjectFile();
        Exitbut.clicked += () => quit();
        Controls.clicked += () => controltoggle();
        //TODO Controls open and hide visual element ontop of menu
    }

    void controltoggle()
    {
        controlmenu.visible =! controlmenu.visible;
    }
    void Menuactive()
    {
        if (Menu.rootVisualElement.visible)
        {
            SceneManager.SetActiveScene(MenuScene);
            Debug.Log("Current Scene:" + MenuScene.name);
            UnityEngine.Cursor.visible = true;
        }
        else if (ProjectScene != null)
        {
            SceneManager.SetActiveScene(ProjectScene);
            Debug.Log("Current Scene:" + ProjectScene.name);
        }
    }
    void openProjectFile() //gets called by the load buttonclick
    {
        if(ProjectScene.name == "empty") //we still have an unused scene with possible use
        {
            SceneManager.UnloadSceneAsync(ProjectScene);
        }

        var bp = new BrowserProperties();
        bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*"; //TODO: Implement Json
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
         {
             Debug.Log("Loading"+ bp +" Json project from" + path);
             ProjectData MyFile = JSONSerializer.Load<ProjectData>(path); //contains file name and extention
             StartProjectFromJson(MyFile.ProjectName);
             JsonFileToProject.LoadData(MyFile); //set project data instance
         });
    }
    public void StartProjectFromJson(string _ProjectName)
    {
        ProjectName = _ProjectName;
        //runs only when loading Debug.Log("");
        if(SceneManager.GetSceneByName("Empty").isLoaded)
        {
            //we probly dont want to have an extra empty scene when we load a json file
            SceneManager.UnloadSceneAsync("Empty"); 
        }
        Startproject();
    }
    void Startproject()
    {
        if(ProjectName == "Empty") //If its a new file populate with serializable objects
        {
            if(!SceneManager.GetSceneByName("Empty").isLoaded)
            {
                ProjectScene = SceneManager.CreateScene(ProjectName); //should use a version number
                SceneManager.SetActiveScene(ProjectScene);
                JsonFileToProject.ProjectFile = new ProjectData();
                JsonFileToProject.ProjectFile.Version = Mathf.Round(0.1f);
                JsonFileToProject.ProjectFile.SceneObjects = new List<GameObjectInScene>();
	            
                ExtensionMethods.MakeStartCube(ProjectScene);
                ExtensionMethods.CreateManager(ProjectScene);
                ExtensionMethods.makebaseplane(ProjectScene);
            }
            else  //Toggle Back to Menu
            {
                Menu.rootVisualElement.visible = false; 
            }
        }
        else //we already should have the data from Json
        {
            
            Debug.Log("Opening: "+ProjectName);

            for (int i = 0; i < SceneManager.sceneCount; i++) 
            {
                if(SceneManager.GetSceneAt(i).name == ProjectName)  //check if scene already exists
                {
                    //Can not create scene until old one is removed
                    Debug.Log("Unloading: " + SceneManager.GetSceneAt(i).name);
                    SceneManager.UnloadSceneAsync(ProjectScene);
                    ProjectName = "Empty";
                    JsonFileToProject.ProjectFile = new ProjectData();
                }
            }
            if(ProjectName != null)
            {
               ProjectScene = SceneManager.CreateScene(ProjectName); //should use a file version number
               SceneManager.SetActiveScene(ProjectScene);
            }
            ExtensionMethods.CreateManager(ProjectScene);
            ExtensionMethods.makebaseplane(ProjectScene);
        }

        toggle();
        FlyCamera.Instance.enabled = true;
        UnityEngine.Cursor.visible = false;
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

    /// <returns>Menu status Enable/Disable</returns>
    public static bool toggleUi() { return Instance.toggle(); Instance.Menuactive(); }

    bool toggle()
    {
      return  Menu.rootVisualElement.visible = !Menu.rootVisualElement.visible;
    }
}
