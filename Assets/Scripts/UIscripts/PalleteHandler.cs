using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class PalleteHandler
{
    public UIDocument Pallete;

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
            // spawn.text = name;  
            spawn.style.backgroundImage = new StyleBackground(image);
            Pallete.rootVisualElement.Q<Foldout>("ContextMenu").Add(spawn);
        }

        for (int i = 1; i < 7; i++) //get context menu buttons assign click function
        {
            string butname = "Button" + i.ToString();
            PrimitiveType mytype =(PrimitiveType) i-1;
            string name = mytype.ToString(); //should change naming
            ICommands.CreatePrefab mycom = new ICommands.CreatePrefab("Prefabs/"+name);
            //CommandInvoker.ExecuteCommand()
            Pallete.rootVisualElement.Q<Button>(butname).clicked += () => mycom.Execute(); //assigns button to a new ICommand
        }
    }
    void GenerateTranslationMenu()
    {
        Label transformlabel;
        transformlabel = new Label();
        transformlabel.style.fontSize = 10;
        transformlabel.name = "transformlabel";

        IntegerField transformX;
        IntegerField transformY;
        IntegerField transformZ;

        transformX = new IntegerField();
        transformY = new IntegerField();
        transformZ = new IntegerField();

        transformX.value = 0;
        transformY.value = 0;
        transformZ.value = 0;

        transformX.label = "X";
        transformY.label = "Y";
        transformZ.label = "Z";

        transformX.style.fontSize = 10;
        transformY.style.fontSize = 10;
        transformZ.style.fontSize = 10;
        transformX.AddToClassList("PalleteStyle");
        transformX.style.maxHeight = 50;
        transformX.style.maxWidth = 200;

        IntegerField Scale;
        Scale = new IntegerField();
        Scale.value = 1;
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
        translation.Add(transformX);
        translation.Add(transformY);
        translation.Add(transformZ);
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
        Scale.RegisterValueChangedCallback(x => GetScaleParameters());
        transformX.RegisterValueChangedCallback(x => SetSelectedParameters());
        transformY.RegisterValueChangedCallback(x => SetSelectedParameters());
        transformZ.RegisterValueChangedCallback(x => SetSelectedParameters());
        Rotation.RegisterValueChangedCallback(x => SetSelectedParameters());

        Debug.Log("Loaded Translation menu");
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
    public static void GetScaleParameters() //Make to Command
    {
        //if (SelectionManager.instance.Current)
        //{
        //    SelectionManager.instance.Current.transform.localScale = new Vector3(Scale.value, Scale.value, Scale.value);
        //    JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, Scale.value);
        //}
    }
    public static void SetSelectedParameters() //Make to Command
    {
        //if (SelectionManager.instance.Current)
        //{
        //    Vector3 position = new Vector3(transformX.value, transformY.value, transformZ.value);
        //    SelectionManager.instance.Current.transform.position = position;
        //    Quaternion rot = new Quaternion
        //        (
        //            Rotation.value.x,
        //            Rotation.value.y,
        //            Rotation.value.z,
        //            Rotation.value.w
        //        );
        //    SelectionManager.instance.Current.transform.rotation = rot;


         //   JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, position);  ///save obj data to json
         //   JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, rot);
        //}
    }
    // Update is called once per frame
    }
