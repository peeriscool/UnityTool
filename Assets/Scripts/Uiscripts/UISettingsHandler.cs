using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
public class UISettingsHandler 
{
    public UIDocument uiSettings ;
    VisualElement myelement;
    DropdownField Historystack;
    List<ICommand> history;
    public UISettingsHandler(UIDocument parent)
    {
        uiSettings = parent;
    }
    public void initialize()
    {
        myelement = new VisualElement();
        myelement.name = "myelement";
        myelement.style.width = 600;
        myelement.style.height = 400;
        myelement.style.backgroundColor = Color.clear;
        Debug.Log(Resources.Load<Sprite>("Ui/ControlsMenu.jpg"));
        myelement.style.backgroundImage = Resources.Load<Texture2D>("Ui/ControlsMenu.jpg");
        myelement.style.backgroundSize = new StyleBackgroundSize(StyleKeyword.Initial);
        uiSettings.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
    }
    public void LoadUi()
    {
      //  if (UiManager.ui.rootVisualElement.Q<VisualElement>("myelement") != null) myelement.Remove(myelement);
        UiManager.ui.rootVisualElement.Add(myelement);
        UpdateHistroy();
    }
    public void UpdateHistroy()
    {
        history = CommandInvoker.GetUndoStack().ToList();
        List<string> options = new List<string>();
        for (int i = 0; i < history.Count; i++)
        {
            ICommand com = CommandInvoker.GetFromStack(i);
            options.Add(com.ToString()+ i.ToString());
        }
        if(Historystack == null)
        {
            Historystack = new DropdownField("History", options, 0);
        }
        Historystack.RegisterValueChangedCallback(handlehistory);
         myelement.Add(Historystack);
        
         Debug.Log("History: " + history.Count);
    }
    private void handlehistory(ChangeEvent<string> evt)
    {
        Debug.Log($"The value has changed to {evt.newValue}.");
        for (int i = 0; i < history.Count; i++)
        {
            if(evt.newValue == history.ElementAt(i) + i.ToString()) //if selected value equals to the type of command specified
            {
                Debug.Log("Selected "+history.ElementAt(i) +" from history: Undo");
                CommandInvoker.GetFromStack(i).Undo();
            //    history.RemoveAt(i);
            //    Historystack.RemoveAt(i);
            }
        }
        myelement.Q<DropdownField>("History");
    }
}
