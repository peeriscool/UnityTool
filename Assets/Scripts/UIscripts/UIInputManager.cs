using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using AnotherFileBrowser.Windows;

public class UIInputManager
{
    private static UIInputManager Instance;
    public UIDocument Menu;
    Scene ProjectScene; //should be moved to different class
    Scene MenuScene; //should be moved to different class

    public UIInputManager(UIDocument Ui)
    {
        MenuScene = SceneManager.GetActiveScene();
        Menu = Ui;
        CreateUI();
        Instance = this;
        Debug.Log(MenuScene);
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

        Newbut.clicked += () => Startproject();
        Loadbut.clicked += () => openProjectFile();
        Exitbut.clicked += () => quit();
    }

    public static void toggleUi()
    {Instance.toggle(); Instance.Menuactive(); }

    void toggle()
    {
        Menu.rootVisualElement.visible = !Menu.rootVisualElement.visible;
    }
    void Menuactive()
    {
        if (Menu.rootVisualElement.visible)
        {
            SceneManager.SetActiveScene(MenuScene);
        }
        else if (ProjectScene != null)
        {
            SceneManager.SetActiveScene(ProjectScene);
        }
    }
    void openProjectFile()
    {

        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*"; //should be json format 
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
         {
                  Debug.Log(path);
                  Debug.Log("Load Json formated project");
             //json.load iets
         });
    }

    void Startproject()
    {
        ProjectScene = SceneManager.CreateScene("ProjectFile_001"); //should use a file version number
        PopulateScene();
        toggle();
        //make project active scene
        SceneManager.SetActiveScene(ProjectScene);
        Program.instance.cameramovement.enabled = true;
        Debug.Log(Program.instance.cameramovement.isActiveAndEnabled);
    }
    void PopulateScene() //runs when creating a new projectfile
    {
        //creating default objects in scene
        SceneManager.MoveGameObjectToScene(GameObject.CreatePrimitive(PrimitiveType.Sphere), ProjectScene);
        SceneManager.MoveGameObjectToScene(GameObject.CreatePrimitive(PrimitiveType.Cube), ProjectScene);
       
        GameObject commandmanager = new GameObject();
        commandmanager.name = "commandmanager";
        UIController  controller = commandmanager.AddComponent<UIController>();
        controller.initialize();
        SceneManager.MoveGameObjectToScene(commandmanager, ProjectScene);

        GameObject baseplane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        baseplane.transform.localScale *= 10;
        baseplane.GetComponent<Collider>().enabled = false; //make ground planen non interactible
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
}
