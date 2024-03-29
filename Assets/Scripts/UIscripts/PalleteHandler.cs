using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class PalleteHandler
{
    public UIDocument Pallete;
    Vector3 lastvalue = new Vector3();  //remembering position value in pallete UI
    public PalleteHandler(UIDocument Owner)
    {
        Pallete = Owner;
    }
    public void initialize()
    {
        //sets the uidocument and panelsettings, loads visualtree asset 
        Pallete.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("Pallete");
        var uiContainer = new VisualElement();
        visualTree.CloneTree(uiContainer); 
        Pallete.rootVisualElement.Add(uiContainer);
    }
    public void LoadUi()
    {
        GenerateButtons();
        GenerateTranslationMenu();  
    }
    void GenerateButtons() //loads 
    {
        for (int i = 1; i < 7; i++)
        {
            string name = "Button" + i.ToString();      
            Button spawn = new Button();
            spawn.name = name;
            spawn.style.width = 100;
            spawn.style.height = 100;
            Sprite image = Resources.Load<Sprite>("Icons/" + name);
            spawn.style.backgroundImage = new StyleBackground(image);
            Pallete.rootVisualElement.Q<Foldout>("ContextMenu").Add(spawn);
        }

        for (int i = 1; i < 7; i++) //get context menu buttons assign click function
        {
            string butname = "Button" + i.ToString();
            PrimitiveType mytype =(PrimitiveType) i-1;
            string name = mytype.ToString(); //should change naming
            ICommands.CreatePrefab mycom = new ICommands.CreatePrefab("Prefabs/"+name);
            //CommandInvoker.ExecuteCommand(mycom)
            Pallete.rootVisualElement.Q<Button>(butname).clicked += () => CommandInvoker.ExecuteCommand(mycom); //mycom.Execute(); //assigns button to a new ICommand
        }
    }
    void GenerateTranslationMenu()
    {
        Label transformlabel; //objectname placeholder
        transformlabel = new Label();
        transformlabel.style.fontSize = 10;
        transformlabel.name = "transformlabel";

        Vector3Field transformfield = new Vector3Field();
        transformfield.name = "transformfield";
        transformfield.value = Vector3.zero;
        transformfield.label = "X-Y-Z";
        transformfield.style.fontSize = 10;

        Vector3Field Scale;
        Scale = new Vector3Field();
        Scale.value = Vector3.one;
        Scale.name = "Scale";
        Scale.label = "Scale";
        Scale.style.fontSize = 10;

        Vector4Field Rotation;
        Rotation = new Vector4Field();
        Rotation.name = "rotatevector";
        Rotation.label = "rotate";
        Rotation.value = new Vector4();
        Rotation.style.fontSize = 10;

        Foldout translation = Pallete.rootVisualElement.Q<Foldout>("Translation");
        translation.SetEnabled(false);
        translation.Add(transformlabel);
        translation.Add(transformfield);
       // translation.Add(transformY);
       /// translation.Add(transformZ);
        translation.Add(Scale);
        translation.Add(Rotation);

        //create buttons
        Button Exportbutton = Pallete.rootVisualElement.Q<Button>("Export");
        Button Savebutton = Pallete.rootVisualElement.Q<Button>("SaveBut");
        Button Import = Pallete.rootVisualElement.Q<Button>("ImportBut");
        Button ImportTexture = Pallete.rootVisualElement.Q<Button>("ImageBut");
        ImportTexture.SetEnabled(false);

        ICommands.ExportCommand Export = new ICommands.ExportCommand();
        ICommands.SaveCommand save = new ICommands.SaveCommand();
        ICommands.ImportCommand import = new ICommands.ImportCommand();
        //add functionality to UI
        Exportbutton.clicked += Export.Execute;
        Savebutton.clicked += save.Execute;
        Import.clicked += import.Execute;
        //ImportTexture.clicked += ImportTextureEvent;
        Scale.RegisterValueChangedCallback(x => SetObjectScaleParameters());
        transformfield.RegisterValueChangedCallback(x => NewMoveCommand());
        Rotation.RegisterValueChangedCallback(x => SetUIParameters());
        Debug.Log("Loaded Translation menu");
    }
    
    private void NewMoveCommand()
    {
        if(Vector3Int.RoundToInt( ExtensionMethods.Round(Pallete.rootVisualElement.Q<Vector3Field>("transformfield").value)) != Vector3Int.RoundToInt(lastvalue))  //only register new position when value is marginaly different with current positon
        {
            Vector3 RoundedPosition = ExtensionMethods.Round(Pallete.rootVisualElement.Q<Vector3Field>("transformfield").value);
            CommandInvoker.ExecuteCommand(new ICommands.SetPositionCommand(RoundedPosition, SelectionManager.GetCurrentTransform()));
            lastvalue = RoundedPosition;
        }
    }
    public void PalleteObjectMenu(string name)
    {
        Pallete.rootVisualElement.Q<Label>("transformlabel").text = name;
    }
    public  void SetPallete(bool status)
    {
        Pallete.rootVisualElement.Q<Foldout>("Translation").SetEnabled(status);
        Pallete.rootVisualElement.Q<Foldout>("Translation").value = status;
    }
    public void SetObjectScaleParameters()
    {
        if (SelectionManager.Current)
        {
            Transform activetransform = SelectionManager.Current.transform;
            Vector3 AplliedScale = Pallete.rootVisualElement.Q<Vector3Field>("Scale").value;
            activetransform.localScale = AplliedScale; //what is value??
            JsonFileToProject.ProjectFile.SetDataFromRefrence(activetransform.name, AplliedScale);
        }
    }
    public void SetUIParameters() //Make to Command
    {
        if (SelectionManager.Current)
        {
            Vector3 position = Pallete.rootVisualElement.Q<Vector3Field>("transformfield").value;
            SelectionManager.Current.transform.position = position;

            Quaternion rot = new Quaternion
                (
                    Pallete.rootVisualElement.Q<Vector4Field>("rotatevector").value.x,
                    Pallete.rootVisualElement.Q<Vector4Field>("rotatevector").value.y,
                    Pallete.rootVisualElement.Q<Vector4Field>("rotatevector").value.z,
                    Pallete.rootVisualElement.Q<Vector4Field>("rotatevector").value.w
                );
            SelectionManager.Current.transform.rotation = rot;

            JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.Current.name, position);  ///save obj data to json
            JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.Current.name, rot);
        }
    }
    public void UpdateUIParameters(Transform activetransform)  //Sets UI Transform and rotation
    {
        if (SelectionManager.Current)
        {
           Pallete.rootVisualElement.Q<Vector3Field>("transformfield").value = ExtensionMethods.Round(activetransform.position);
            Pallete.rootVisualElement.Q<Vector4Field>("rotatevector").value = new Vector4
            (
                activetransform.rotation.x,
                activetransform.rotation.y,
                activetransform.rotation.z,
                activetransform.rotation.w
            );
            //JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, SelectionManager.instance.transform.position);  ///save obj data to json
        }
    }
    
}
