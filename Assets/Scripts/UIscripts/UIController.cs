using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using AnotherFileBrowser.Windows;
using UnityEngine.EventSystems;
using Dummiesman;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
/// <summary>
/// Curent functionality:
/// loads a UIDocument to the scene
/// Populate UI with object buttons
/// Handle functions from the panel UI
/// </summary>
class UIController : MonoBehaviour
    {
    //static IntegerField transformX;
    //static IntegerField transformY;
    //static  IntegerField transformZ;
    //static IntegerField Scale;
    //static  Vector4Field Rotation;
    //static Label transformlabel;
    //static Foldout foldoutmenu;
    //static Foldout transformation;
    //UIDocument Mydocument;

    //public void initialize()
    //{
    //    Mydocument = this.gameObject.AddComponent<UIDocument>();
    //    Mydocument.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
    //    VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("Pallete");

    //    if (visualTree != null)
    //    {
    //        var uiContainer = new VisualElement();
    //        visualTree.CloneTree(uiContainer);
    //        Mydocument.rootVisualElement.Add(uiContainer);
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to load UXML file: " + "Pallete");
    //    }
    //    LoadUI();
    //}
    //void LoadUI()
    //{
    //    VisualElement root = GetComponent<UIDocument>().rootVisualElement;
    //    //TODOHistory
        
    //    foldoutmenu = root.Q<Foldout>("ContextMenu");
    //    for (int i = 1; i < 7; i++)
    //    {
    //        string name = "Button" + i.ToString();
    //        Button spawn = new Button();
    //        spawn.name = name;
    //        spawn.style.width = 100;
    //        spawn.style.height = 100;
    //        Sprite image = Resources.Load<Sprite>("Icons/"+ name);
    //        // spawn.text = name;  
    //        spawn.style.backgroundImage = new StyleBackground(image);
    //        foldoutmenu.Add(spawn);
    //    }
    //    ///https://forum.unity.com/threads/subscribing-a-method-with-parameter-to-an-action.1054676/
    //    for (int i = 1; i < 7; i++) //get context menu buttons assign click function
    //    {
    //        string name = "Button" + i.ToString();
    //        root.Q<Button>(name).clicked +=()  => SpawnObject(name);
    //    }
    //    //Pallete
    //    transformlabel = new Label();
    //    transformlabel.style.fontSize = 10;
    //    transformlabel.name = "transform vector";
    //    transformX = new IntegerField();
    //    transformY = new IntegerField();
    //    transformZ = new IntegerField();

    //    transformX.value = 0;
    //    transformY.value = 0;
    //    transformZ.value = 0;

    //    transformX.label = "X";
    //    transformY.label = "Y";
    //    transformZ.label = "Z";

    //    transformX.style.fontSize = 10;
    //    transformY.style.fontSize = 10;
    //    transformZ.style.fontSize = 10;
    //    transformX.AddToClassList("PalleteStyle");
    //    transformX.style.maxHeight = 50;
    //    transformX.style.maxWidth = 200;

    //    Scale = new IntegerField();
    //    Scale.value = 1;
    //    Scale.label = "Scale";
    //    Scale.style.fontSize = 10;

    //    Rotation = new Vector4Field();
    //    Rotation.name = "rotatevector";
    //    Rotation.label = "rotate";
    //    Rotation.value = new Vector4();
    //    Rotation.style.fontSize = 10;
        
    //    transformation = root.Q<Foldout>("Translation");
    //    transformation.SetEnabled(false);
    //    transformation.value = false;   
    //    //  transformation.Add(Scale);
    //    transformation.Add(transformlabel);
    //    transformation.Add(transformX);
    //    transformation.Add(transformY);
    //    transformation.Add(transformZ);
    //    transformation.Add(Scale);
    //    transformation.Add(Rotation);
       
    //    //create buttons
    //    Button Exportbutton = root.Q<Button>("Export");
    //    Button Savebutton = root.Q<Button>("SaveBut");
    //    Button Import = root.Q<Button>("ImportBut");
    //    Button ImportTexture = root.Q<Button>("ImageBut");
    //    ImportTexture.SetEnabled(false);

    //        //add functionality to UI
    //    Exportbutton.clicked += ExportEvent;
    //    Savebutton.clicked += SaveEvent;
    //    Import.clicked += ImportEvent;
    //    ImportTexture.clicked += ImportTextureEvent;
    //    Scale.RegisterValueChangedCallback(x => GetScaleParameters());
    //    transformX.RegisterValueChangedCallback(x => SetSelectedParameters());
    //    transformY.RegisterValueChangedCallback(x => SetSelectedParameters());
    //    transformZ.RegisterValueChangedCallback(x => SetSelectedParameters());
    //    Rotation.RegisterValueChangedCallback(x => SetSelectedParameters());
    //}
  
    ////----------------------------------------------------------------------------Has  nothing to do with UI therefor it should be moved to different class-------------------------------------------------------------------------------------------\\
    //private static void SpawnObject(string name) ////should be replaced by create prefab command should be made more dynamic, without direct object definition
    //{
    //    if(name == "Button1") { GameObject Plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
    //        GameObjectInScene obj = new GameObjectInScene(Plane);
    //        JsonFileToProject.AddObject(obj);
    //    }
    //    if (name == "Button2") { GameObject Capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
    //        GameObjectInScene obj = new GameObjectInScene(Capsule);
    //        JsonFileToProject.AddObject(obj);
    //    }
    //    if (name == "Button3") { GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
    //        GameObjectInScene obj = new GameObjectInScene(Cube);
    //        JsonFileToProject.AddObject(obj);
    //    }
    //    if (name == "Button4") { GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder); 
    //        GameObjectInScene obj = new GameObjectInScene(Cylinder);
    //        JsonFileToProject.AddObject(obj);
    //    }
    //    if (name == "Button5") { GameObject Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
    //        GameObjectInScene obj = new GameObjectInScene(Quad);
    //        JsonFileToProject.AddObject(obj);
    //    }
    //    if (name == "Button6") { GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //        GameObjectInScene obj = new GameObjectInScene(Sphere);
    //        JsonFileToProject.AddObject(obj);
    //    }
    //    Debug.Log(name);
    //}

    //private void ImportEvent() //functionality to import new obj 
    //{
    //    GameObject ImportedObject = new GameObject();
    //    //Action<string> action = (pat);
    //    OBJLoader ObjModel = new OBJLoader();
    //    var bp = new BrowserProperties();
    //    bp.filter = "Obj files (*.obj)|*.obj|All Files (*.*)|*.*"; //TODO:mtl
    //    bp.filterIndex = 0;

    //    //lambda expresion Anonimus function
    //    //https://learn.microsoft.com/nl-nl/dotnet/csharp/language-reference/operators/lambda-expressions
    //    new FileBrowser().OpenFileBrowser(bp, path  =>
    //    {
    //        Debug.Log("Loading" + bp + " Json project from" + path);
    //        ImportedObject = ObjModel.Load(path);
    //    });
    //    MeshRenderer render = ImportedObject.AddComponent<MeshRenderer>();
    //    MeshFilter filter;

    //    if (ImportedObject.TryGetComponent<MeshFilter>(out MeshFilter _filter)) //check if mesh comes with meshfilter
    //    {
    //        filter = _filter;
    //    }
    //    else //make empty filter
    //    {
    //        //try  to get from child
    //        if(ImportedObject.GetComponentInChildren<MeshFilter>())
    //        {
    //            filter = ImportedObject.GetComponentInChildren<MeshFilter>();
    //        }
    //        else
    //        {
    //            Debug.Log("Adding meshfilter to root");
    //            filter = ImportedObject.AddComponent<MeshFilter>();
    //        }
    //    }
    //    ExtensionMethods.AddMeshCollider(ImportedObject);
    //    //GenerateBoxcolliderOnMesh(ImportedObject, filter);
    //    JsonFileToProject.ProjectFile.SceneObjects.Add(new GameObjectInScene(ImportedObject,filter));
    //} 
    //private void ImportTextureEvent()
    //{
    //   // FileBrowserUpdate util = this.gameObject.AddComponent<FileBrowserUpdate>();
    //   // util.OpenFileBrowser();
    //}
    
    //private void SaveEvent()
    //{
    //    var bp = new BrowserProperties();
    //    bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*";
    //    bp.filterIndex = 0;
        
    //    new FileBrowser().SaveFileBrowser(bp,"ProjectFile_01",".json" , SavePath =>
    //     {
    //         JsonFileToProject.ProjectFile.ProjectName = Path.GetFileName(SavePath);
    //         JSONSerializer.Save(SavePath, JsonFileToProject.ProjectFile);

    //     });
    //}
    //private void ExportEvent()
    //{
    //    List<GameObject> exportmeshes = new List<GameObject>();
    //    for (int i = 0; i < JsonFileToProject.ProjectFile.SceneObjects.Count; i++)
    //    {
    //        if(JsonFileToProject.ProjectFile.SceneObjects[i].Getrefrence() != null) exportmeshes.Add(JsonFileToProject.ProjectFile.SceneObjects[i].Getrefrence());
    //    }
    //       string path = openProjectFile();
    //    ObjExporterStandalone exportUtil = new ObjExporterStandalone();
    //    exportUtil.Export(path, exportmeshes);
    //}
    //string openProjectFile()
    //{
    //    string sendpath = "";
    //    var bp = new BrowserProperties();
    //    bp.filter = "obj files (*.obj)|*.obj|All Files (*.*)|*.*";
    //    bp.filterIndex = 0;

        
    //    new FileBrowser().SaveFileBrowser(bp, "MyLevelExport",bp.filter,path =>
    //    {
    //        //   Load Binary or Json format of project
    //        Debug.Log(path);
    //        Debug.Log("Load Json project");
    //        sendpath = path;

    //    });
    //    return sendpath;
    //}
    //public static void GetScaleParameters() //Sets Current object scale
    //{
    //    if (SelectionManager.instance.Current)
    //    {
    //        SelectionManager.instance.Current.transform.localScale = new Vector3(Scale.value, Scale.value, Scale.value);
    //        JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, Scale.value);
    //    }
    //}
    //public static void SetSelectedParameters()  //Sets Current object transform and rotation
    //{
    //    if (SelectionManager.instance.Current)
    //    {
    //        Vector3 position = new Vector3(transformX.value, transformY.value, transformZ.value);
    //        SelectionManager.instance.Current.transform.position = position;
    //        Quaternion rot = new Quaternion
    //            (
    //                Rotation.value.x,
    //                Rotation.value.y,
    //                Rotation.value.z,
    //                Rotation.value.w
    //            );
    //        SelectionManager.instance.Current.transform.rotation = rot;


    //        JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, position);  ///save obj data to json
    //        JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, rot);
    //    }
    //}
    ////------------------------------------------------------End of code that needs to be moved-----------------------------------------------------------------------------------------------------------------\\

    //public static void UpdateUIParameters()  //Sets UI Transform and rotation
    //{
    //    if (SelectionManager.instance.Current)
    //    {
    //        transformX.value = (int)SelectionManager.instance.Current.transform.position.x;
    //        transformY.value = (int)SelectionManager.instance.Current.transform.position.y;
    //        transformZ.value = (int)SelectionManager.instance.Current.transform.position.z;
    //        Rotation.value = new Vector4
    //        (
    //            SelectionManager.instance.Current.transform.rotation.x,
    //            SelectionManager.instance.Current.transform.rotation.y,
    //            SelectionManager.instance.Current.transform.rotation.z,
    //            SelectionManager.instance.Current.transform.rotation.w
    //        );
    //         //JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, SelectionManager.instance.transform.position);  ///save obj data to json
    //    }
    //}
 
    //public static void SetScaleParameters() //Sets UI object scale
    //{
    //    if (SelectionManager.instance.Current)
    //    {
    //    Scale.value = (int) SelectionManager.instance.Current.transform.localScale.x;
    //    }
    //}
 
    //public static void PalleteObjectMenu(string name)
    //{
    //    transformlabel.text = name;
    //}
    //public static void SetPallete(bool status)
    //{
    //    transformation.SetEnabled(status);
    //    transformation.value = status;  
    //}
}
