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
    public UIInputManager(UIDocument Ui)
    {
        Menu = Ui;
        CreateUI();
        Instance = this;
    }
    private void CreateUI()
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
    { Instance.toggle(); }
    void toggle()
    {
        Menu.enabled = !Menu.enabled;
    }
    void openProjectFile()
    {
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
         {
                  //Load Binary or Json format of project
                  Debug.Log(path);
             Debug.Log("Load Binary or Json format of project");
         });
    }
    void Startproject()
    {
        //   UnityEngine.SceneManagement.SceneManager.LoadScene("PeerSampleScene");
       ProjectScene = SceneManager.CreateScene("ProjectFile_001");
       Menu.enabled = false; //ToDo enable by esc
       SceneManager.MoveGameObjectToScene(GameObject.CreatePrimitive(PrimitiveType.Sphere), ProjectScene);
       SceneManager.MoveGameObjectToScene(GameObject.CreatePrimitive(PrimitiveType.Cube), ProjectScene);
        GameObject baseplane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        baseplane.transform.localScale *= 10;
        baseplane.GetComponent<Collider>().enabled = false;
        SceneManager.MoveGameObjectToScene(baseplane, ProjectScene);
        SceneManager.SetActiveScene(ProjectScene);
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
