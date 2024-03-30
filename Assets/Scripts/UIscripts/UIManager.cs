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
    public static UIDocument ui; //can load UIelements from handlers
    public PalleteHandler palleteHandler;
    public StartMenuHandler menuHandler;
    public UISettingsHandler uisettings;
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
        uisettings = new UISettingsHandler(ui);
        uisettings.initialize();
        palleteHandler.initialize(); //sets uidocument to pallete
        menuHandler.initialize(); //sets uidocument to start menu
            
        activeUI = "StarMenuUi"; //start with menu menuHandler.Menu.visualTreeAsset.name
        Debug.Log(menuHandler.Menu.visualTreeAsset.name +"_"+ palleteHandler.Pallete.visualTreeAsset.name + "_" + uisettings.uiSettings.visualTreeAsset.name);
        StartMenuHandler();
    }

    public void CallPalleteHandler()
    {
        if (activeUI != "Pallete")
        {
            ClearMenu();
            palleteHandler.LoadUi();
            //ui = palleteHandler.Pallete;
            activeUI = "Pallete";// ui.visualTreeAsset.name;
        }
    }
    public void StartMenuHandler()
    {
            ClearMenu();
            menuHandler.LoadUi();
            // ui = menuHandler.Menu;
            activeUI = "StarMenuUi";//ui.visualTreeAsset.name;
    }
    public void Calluisettings()
    {
        if (activeUI != "uiSettings")
        {
            ClearMenu();
            uisettings.LoadUi();
            // ui = uisettings.uiSettings;
            activeUI = "uiSettings";
        }
    }
    public void ClearMenu()
    {
        ui.rootVisualElement.Clear();
    }
}
