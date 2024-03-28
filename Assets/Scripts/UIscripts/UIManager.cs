using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Konws which menu is active
/// can handle UIelement handlers
/// 
/// </summary>
/// 
[Serializable]
public class UiManager 
{
    UIDocument ui; //can load UIelements from handlers
    public PalleteHandler palleteHandler;
    public StartMenuHandler menuHandler;
    public string activeUI;
    public UiManager(UIDocument document)
    {
        ui = document;
        ui.rootVisualElement.Clear(); //start empty even if we have values in the document
        // activeUI = ui.visualTreeAsset.name;
        
    }

    public void StartApp()
    {
        palleteHandler = new PalleteHandler(ui);
        menuHandler = new StartMenuHandler(ui);
        activeUI = "StarMenuUi"; //start with menu menuHandler.Menu.visualTreeAsset.name
        Debug.Log(menuHandler.Menu.visualTreeAsset.name +"_"+ palleteHandler.Pallete.visualTreeAsset.name + "_" + ui.visualTreeAsset.name);
        StartMenuHandler();
    }

    public void CallPalleteHandler()
    {
        ClearMenu();
        palleteHandler.initialize(); //sets uidocument to pallete
        palleteHandler.LoadUi();
        ui = palleteHandler.Pallete;
        activeUI = "Pallete";// ui.visualTreeAsset.name;
        
    }
    public void StartMenuHandler()
    {
        ClearMenu();
        menuHandler.initialize(); //sets uidocument to start menu
        menuHandler.AssignButtonValues();
        ui = menuHandler.Menu;
        activeUI = "StarMenuUi";//ui.visualTreeAsset.name;
    }
    public void ClearMenu()
    {
        ui.rootVisualElement.Clear();
    }
}
