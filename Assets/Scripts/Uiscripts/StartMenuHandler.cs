using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenuHandler
{
    private static StartMenuHandler Instance; //singleton
    public UIDocument Menu;
    //Commands
    ICommands.OpenProjctFile ButtonOpen = new ICommands.OpenProjctFile();
    ICommands.NewFile ButtonNew;
    public StartMenuHandler(UIDocument Ui)
    {
        Menu = Ui;
        Instance = this;
        Debug.Log("Current Scene:" + SceneManager.GetActiveScene().name);
    }
    public void initialize()
    {
        //sets the uidocument and panelsettings, loads visualtree asset 
        Menu.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("StarMenuUi");
        var uiContainer = new VisualElement();
        visualTree.CloneTree(uiContainer);
        Menu.rootVisualElement.Add(uiContainer);
    }
    public void AssignButtonValues()
    {
        Menu.rootVisualElement.visible = true;
        Menu.rootVisualElement.SetEnabled(true);
        Button Newbut = Menu.rootVisualElement.Q<Button>("New");
        Button Loadbut = Menu.rootVisualElement.Q<Button>("Load");
        Button Exitbut = Menu.rootVisualElement.Q<Button>("Exit");
        Button Controls = Menu.rootVisualElement.Q<Button>("Control");

        Menu.rootVisualElement.Q<VisualElement>("ControlsMenu").visible = false;
        ButtonNew = new ICommands.NewFile(SceneManager.GetActiveScene(), "Empty");
        //assign functions to funvtions
        Newbut.clicked += () => ButtonNew.Execute();
        Newbut.clicked += () => Program.instance.Uimanager.CallPalleteHandler();
        Loadbut.clicked += () => ButtonOpen.Execute();
        Loadbut.clicked += () => Program.instance.Uimanager.CallPalleteHandler();
        Exitbut.clicked += () => quit();
        Controls.clicked += () => controltoggle();
    }
    private void quit()
    {
        //if editor 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //if apllication
        Application.Quit();
    }
    void controltoggle()
    {
        Menu.rootVisualElement.Q<VisualElement>("ControlsMenu").visible = !Menu.rootVisualElement.Q<VisualElement>("ControlsMenu").visible;
    }
}
