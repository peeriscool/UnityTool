using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenuHandler
{
    public UIDocument Menu;
    VisualElement MenuElement = new VisualElement();
    ICommands.OpenProjctFile ButtonOpen = new ICommands.OpenProjctFile();
    ICommands.NewFile ButtonNew;
   
    public StartMenuHandler(UIDocument Ui)
    {
        Menu = Ui;
       // MenuElement = Ui.rootVisualElement;
        Debug.Log("Current Scene:" + SceneManager.GetActiveScene().name);
    }
    public void initialize()
    {
        //sets the uidocument and panelsettings, loads visualtree asset 
        Menu.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("StarMenuUi");
        var uiContainer = new VisualElement();
        visualTree.CloneTree(uiContainer);
        MenuElement.Add(uiContainer);

        Button Newbut = MenuElement.Q<Button>("New");
        Button Loadbut = MenuElement.Q<Button>("Load");
        Button Exitbut = MenuElement.Q<Button>("Exit");
        Button Controls = MenuElement.Q<Button>("Control");

        MenuElement.Q<VisualElement>("ControlsMenu").visible = false;
        ButtonNew = new ICommands.NewFile(SceneManager.GetActiveScene(), "Empty");
        //assign functions to funvtions
        Newbut.clicked += () => ButtonNew.Execute();
        Newbut.clicked += () => Program.instance.Uimanager.CallPalleteHandler();
        Loadbut.clicked += () => ButtonOpen.Execute();
        Loadbut.clicked += () => Program.instance.Uimanager.CallPalleteHandler();
        Exitbut.clicked += () => quit();
        Controls.clicked += () => controltoggle();


    }
    public void LoadUi()
    {
        UiManager.ui.rootVisualElement.Add(MenuElement);
        MenuElement.visible = true;
        MenuElement.SetEnabled(true);
       
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
        MenuElement.Q<VisualElement>("ControlsMenu").visible = !MenuElement.Q<VisualElement>("ControlsMenu").visible;
    }
}
