using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UISettingsHandler 
{
    public UIDocument uiSettings ;
    VisualElement myelement;    
    public UISettingsHandler(UIDocument parent)
    {
        uiSettings = parent;
    }
    public void initialize()
    {
        myelement = new VisualElement();
        myelement.style.width = 400;
        myelement.style.height = 1080;
        myelement.style.backgroundColor = Color.clear;
        Debug.Log(Resources.Load<Sprite>("Ui/ControlsMenu.jpg"));
        myelement.style.backgroundImage = Resources.Load<Texture2D>("Ui/ControlsMenu.jpg");
        myelement.style.backgroundSize = new StyleBackgroundSize(StyleKeyword.Auto);
        uiSettings.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
        myelement.style.width = 300;
        myelement.style.height = 300;
        myelement.style.color = Color.red;
    }
    public void LoadUi()
    {
        UiManager.ui.rootVisualElement.Clear();
        UiManager.ui.rootVisualElement.Add(myelement);
        UpdateHistroy();
    }
    public void UpdateHistroy()
    {
        Stack<ICommand> history = CommandInvoker.GetUndoStack();
        List<string> options = new List<string>();
        for (int i = 0; i < history.Count; i++)
        {
            ICommand com = CommandInvoker.GetFromStack(i);
            options.Add(com.ToString()+ i.ToString());
        }
        DropdownField Historystack = new DropdownField("History", options, 0);
        uiSettings.rootVisualElement.Add(Historystack);
        Debug.Log("History: " + history.Count);
    }

}
