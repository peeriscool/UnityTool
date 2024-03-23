using System.Collections;
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
    VisualElement root = new VisualElement();

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
    void openProjectFile() //gets called by the load buttonclick
    {
        if(ProjectScene.name == "empty") //we still have an unused scen with possably use
        {
            SceneManager.UnloadSceneAsync(ProjectScene);
        }

        var bp = new BrowserProperties();
        bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*"; //TODO: Implement Json
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
         {
             Debug.Log("Loading"+ bp +" Json project from" + path);
             //json.load iets
             ProjectData MyFile = JSONSerializer.Load<ProjectData>(path); //contains file name and extention
             StartProjectFromJson(MyFile.ProjectName);
             JsonFileToProject.LoadData(MyFile); //set project data instance

         });
    }
    public void StartProjectFromJson(string _ProjectName)
    {
        ProjectName = _ProjectName;
        //runs only when loading Debug.Log("");
        Startproject();
    }
    void Startproject()
    {
        
        if(ProjectName == "Empty") //If its a new file populate with serializable objects
        {
          
            Debug.Log("New Empty_File, Fresh ProjectData");
            ProjectScene = SceneManager.CreateScene(ProjectName); //should use a file version number
            SceneManager.SetActiveScene(ProjectScene);
            JsonFileToProject.ProjectFile = new ProjectData();
            JsonFileToProject.ProjectFile.ProjectName = ProjectName;
            JsonFileToProject.ProjectFile.Version = 0.1f;
            JsonFileToProject.ProjectFile.SceneObjects = new List<GameObjectInScene>();
            PopulateScene();
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
            CreateManager();
            makebaseplane();
        }
        toggle();
        //make project active scene
     
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
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObjectInScene startcube = new GameObjectInScene(cube);
        //GameObjectInScene startcube = new GameObjectInScene("StartCube",Vector3.one,Vector3.zero,Quaternion.identity,PrimitiveType.Cube);
        JsonFileToProject.AddObject(startcube); //make it saveable
        //GameObject a = JsonFileToProject.ProjectFile.SceneObjects;
        SceneManager.MoveGameObjectToScene(JsonFileToProject.ProjectFile.GetItemFromList(startcube), ProjectScene);
        
          ///  SceneManager.MoveGameObjectToScene(GameObject.CreatePrimitive(PrimitiveType.Cube), ProjectScene)) ;
        CreateManager();
        makebaseplane();


    }
    void makebaseplane ()
    {
        //make sure plane does not get exported to obj
        GameObject baseplane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        baseplane.transform.localScale *= 4;
        baseplane.name = "BasePlane";
        baseplane.GetComponent<Collider>().enabled = false; //make ground planen non interactible
        baseplane.GetComponent<Renderer>().material = Resources.Load<Material>("GridMaterial");
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
