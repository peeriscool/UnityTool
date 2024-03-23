using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using AnotherFileBrowser.Windows;
using UnityEngine.EventSystems;
using Dummiesman;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
//loads a UIDocument to the scene
class UIController : MonoBehaviour
    {
  static IntegerField transformX;
  static IntegerField transformY;
  static  IntegerField transformZ;
    static IntegerField Scale;
    static  Vector4Field Rotation;
  static Label transformlabel;
    static Foldout foldoutmenu;
  static Foldout transformation;
    UIDocument Mydocument;
    public void initialize()
    {
        Mydocument = this.gameObject.AddComponent<UIDocument>();
        Mydocument.panelSettings = Resources.Load<PanelSettings>("PanelSettings");
        VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("Pallete");

        if (visualTree != null)
        {
            var uiContainer = new VisualElement();
            visualTree.CloneTree(uiContainer);
            Mydocument.rootVisualElement.Add(uiContainer);
        }
        else
        {
            Debug.LogError("Failed to load UXML file: " + "Pallete");
        }
        LoadUI();
    }
    void LoadUI()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        //TODOHistory
        //COntextMenu
        //get contextMenu
        //load icons from resources, assign as button value
        foldoutmenu = root.Q<Foldout>("ContextMenu");
        for (int i = 1; i < 7; i++)
        {
            string name = "Button" + i.ToString();
            Button spawn = new Button();
            spawn.name = name;
            spawn.style.width = 100;
            spawn.style.height = 100;
            Sprite image = Resources.Load<Sprite>("Icons/"+ name);
         ///   spawn.text = name;  
            spawn.style.backgroundImage = new StyleBackground(image);
            foldoutmenu.Add(spawn);
        }
        ///https://forum.unity.com/threads/subscribing-a-method-with-parameter-to-an-action.1054676/
        for (int i = 1; i < 7; i++) //getcontextmenubuttons assign click function
        {
            string name = "Button" + i.ToString();
            root.Q<Button>(name).clicked +=()  => SpawnObject(name);
        }
        //Pallete
        transformlabel = new Label();
        transformlabel.style.fontSize = 10;
        transformlabel.name = "transform vector";
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

        Scale = new IntegerField();
        Scale.value = 1;
        Scale.label = "Scale";
        Scale.style.fontSize = 10;

        Rotation = new Vector4Field();
        Rotation.name = "rotatevector";
        Rotation.label = "rotate";
        Rotation.value = new Vector4();
        Rotation.style.fontSize = 10;
        
    
        transformation = root.Q<Foldout>("Translation");
        transformation.SetEnabled(false);
        transformation.value = false;   
      //  transformation.Add(Scale);
        transformation.Add(transformlabel);
        transformation.Add(transformX);
        transformation.Add(transformY);
        transformation.Add(transformZ);
        transformation.Add(Scale);
        transformation.Add(Rotation);
       
        //create buttons
        Button Exportbutton = root.Q<Button>("Export");
        Button Savebutton = root.Q<Button>("SaveBut");
        Button Import = root.Q<Button>("ImportBut");
        Button ImportTexture = root.Q<Button>("ImageBut");


      
            //add functionality to UI
        Exportbutton.clicked += ExportEvent;
        Savebutton.clicked += SaveEvent;
        Import.clicked += ImportEvent;
        ImportTexture.clicked += ImportTextureEvent;
        Scale.RegisterValueChangedCallback(x => GetScaleParameters());
        transformX.RegisterValueChangedCallback(x => SetSelectedParameters());
        transformY.RegisterValueChangedCallback(x => SetSelectedParameters());
        transformZ.RegisterValueChangedCallback(x => SetSelectedParameters());
        Rotation.RegisterValueChangedCallback(x => SetSelectedParameters());
    }
    private static void SpawnObject(string name)
    {
        if(name == "Button1") { GameObject Plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GameObjectInScene obj = new GameObjectInScene(Plane);
            JsonFileToProject.AddObject(obj);
        }
        if (name == "Button2") { GameObject Capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            GameObjectInScene obj = new GameObjectInScene(Capsule);
            JsonFileToProject.AddObject(obj);
        }
        if (name == "Button3") { GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
            GameObjectInScene obj = new GameObjectInScene(Cube);
            JsonFileToProject.AddObject(obj);
        }
        if (name == "Button4") { GameObject Cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder); 
            GameObjectInScene obj = new GameObjectInScene(Cylinder);
            JsonFileToProject.AddObject(obj);
        }
        if (name == "Button5") { GameObject Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            GameObjectInScene obj = new GameObjectInScene(Quad);
            JsonFileToProject.AddObject(obj);
        }
        if (name == "Button6") { GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObjectInScene obj = new GameObjectInScene(Sphere);
            JsonFileToProject.AddObject(obj);
        }
      
        Debug.Log(name);
    }
    private void ImportEvent()
    {
        GameObject ImportedObject = new GameObject();
        //Action<string> action = (pat);
        OBJLoader ObjModel = new OBJLoader();
        var bp = new BrowserProperties();
        bp.filter = "Obj files (*.obj)|*.obj|All Files (*.*)|*.*"; //TODO:mtl
        bp.filterIndex = 0;

        //lambda expresion Anonimus function
        //https://learn.microsoft.com/nl-nl/dotnet/csharp/language-reference/operators/lambda-expressions
        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            Debug.Log("Loading" + bp + " Json project from" + path);
            ImportedObject = ObjModel.Load(path);
        });
        MeshRenderer render = ImportedObject.AddComponent<MeshRenderer>();
        MeshFilter filter;

        if (ImportedObject.TryGetComponent<MeshFilter>(out MeshFilter _filter)) //check if mesh comes with meshfilter
        {
            filter = _filter;
        }
        //else if(ImportedObject.GetComponentInChildren<MeshFilter>())//see if childeren of mesh have filter
        //{
        //    filter = ImportedObject.GetComponentInChildren<MeshFilter>();
        //}
        else //make empty filter
        {
            Debug.Log("Adding meshfilter to root");
             filter = ImportedObject.AddComponent<MeshFilter>();
        }
        
       //  
       // BoxCollider Mycollider = ImportedObject.AddComponent<BoxCollider>(); //make it so we can select it with the selectionmanager

        GenerateBoxcolliderOnMesh(ImportedObject, filter);
        ImportedObject.AddComponent<MoveableBehaviour>();
    } 
    private void ImportTextureEvent()
    {
        FileBrowserUpdate util = this.gameObject.AddComponent<FileBrowserUpdate>();
        util.OpenFileBrowser();
    }
    
    /// <summary>
    /// NOT OPTIMIZED!!! Fixes the colliders when pivot point is not on mesh location
    /// </summary>
    /// <param name="ImportedObject"></param>
    public void GenerateBoxcolliderOnMesh(GameObject ImportedObject,MeshFilter filter)
    {
       
        if (ImportedObject != null)
        {
            BoxCollider Mycollider = ImportedObject.AddComponent<BoxCollider>(); //make it so we can select it with the selectionmanager
            int Submeshcount = ImportedObject.transform.childCount;
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;
            Vector3 center = new Vector3();

            //used if we need the childeren to calculate the colliders
           List<Vector3> allmin = new List<Vector3>();
            List<Vector3> allmax = new List<Vector3>();

            if (filter.mesh.vertices.Length > 0) //use root to calculate boxcolider
            {
                Debug.Log("Running through" + " meshFilter for center");
                for (int i = 0; i < filter.mesh.vertices.Length; i++)
                {
                    min = Vector3.Min(filter.mesh.vertices[i], min);
                    max = Vector3.Max(filter.mesh.vertices[i], max);
                    center += filter.mesh.vertices[i];
                }
                center.x = center.x / filter.mesh.vertices.Length;
                center.y = center.y / filter.mesh.vertices.Length;
                center.z = center.z / filter.mesh.vertices.Length;
            }
            else if (filter.mesh.vertices.Length == 0 && Submeshcount != 0) //root object does not have any mesh data
            {
                Debug.Log("Running through" + Submeshcount + " child meshes");
                //generate collider location on childeren
                for (int i = 0; i < Submeshcount; i++)
                {
                    Transform child = ImportedObject.transform.GetChild(i);
                    Mesh childmesh = child.GetComponent<MeshFilter>().mesh;

                    if (childmesh != null)
                    {
                        Debug.Log("Running through" + childmesh.vertices.Length + " vertices");
                        for (int j = 0; j < childmesh.vertices.Length; j++)
                        {
                            //   Debug.Log(childmesh.vertices[j]);
                            min = Vector3.Min(childmesh.vertices[j], min);
                            max = Vector3.Max(childmesh.vertices[j], max);
                            allmin.Add(min);
                            allmax.Add(max);
                            center += childmesh.vertices[j];
                        }
                    }
                    for (int x = 1; x < allmax.Count; x++)
                    {
                        max = Vector3.Max(allmax[x], allmax[x-1]);
                    }
                    //   center.x = center.x / childmesh.vertices.Length;
                    //  center.y = center.y / childmesh.vertices.Length;
                    //   center.z = center.z / childmesh.vertices.Length;
                    center = Vector3.zero;
                    Debug.Log(childmesh.name + " minimal " + min + "root = " + ImportedObject.name);
                    Debug.Log(childmesh.name + " maximum " + max + "root = " + ImportedObject.name);
                }
            }
            else //meshfilter is empty
            {
                max = Vector3.one;
            }

            Vector3 size = max;//Vector3.Max(min, max);
            size.x = size.z;
            Mycollider.size = size; //X value seems to be 0

            Mycollider.center = center;
            Debug.Log("center " + center);
        }
    }
    private void SaveEvent()
    {

        var bp = new BrowserProperties();
        bp.filter = "Json files (*.Json)|*.Json|All Files (*.*)|*.*";
        bp.filterIndex = 0;
        

        new FileBrowser().SaveFileBrowser(bp,"ProjectFile_01",".json" , SavePath =>
         {
             JsonFileToProject.ProjectFile.ProjectName = Path.GetFileName(SavePath);
             JSONSerializer.Save(SavePath, JsonFileToProject.ProjectFile);

         });
        //temp savedata
       
    }
    private void ExportEvent()
    {
        //  SavePrefab.Save(GameObject.CreatePrimitive(PrimitiveType.Cube));
        List<GameObject> exportmeshes = new List<GameObject>();
        exportmeshes.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        exportmeshes.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));

        string path = openProjectFile();
        SavePrefab.ObjExportUtil(path, exportmeshes);
    }
    string openProjectFile()
    {
        string sendpath = "";
        var bp = new BrowserProperties();
        bp.filter = "obj files (*.obj)|*.obj|All Files (*.*)|*.*";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //   Load Binary or Json format of project
            Debug.Log(path);
            Debug.Log("Load Json project");
            sendpath = path;

        });
        return sendpath;
    }
    public static void UpdateUIParameters()  //Sets UI Transform and rotation
    {
        if (SelectionManager.instance.Current)
        {
            transformX.value = (int)SelectionManager.instance.Current.transform.position.x;
            transformY.value = (int)SelectionManager.instance.Current.transform.position.y;
            transformZ.value = (int)SelectionManager.instance.Current.transform.position.z;
            Rotation.value =
                new Vector4
                (
                    SelectionManager.instance.Current.transform.rotation.x,
                    SelectionManager.instance.Current.transform.rotation.y,
                    SelectionManager.instance.Current.transform.rotation.z,
                    SelectionManager.instance.Current.transform.rotation.w
                );

         //   JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, SelectionManager.instance.transform.position);  ///save obj data to json
         //   JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, SelectionManager.instance.transform.rotation);
        }
       
    }
    public static void GetScaleParameters() //Sets Current object scale
    {
        if(SelectionManager.instance.Current)
        {
           SelectionManager.instance.Current.transform.localScale = new Vector3 (Scale.value, Scale.value, Scale.value);
            JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, Scale.value);
        }
    }
    public static void SetScaleParameters() //Sets UI object scale
    {
        if (SelectionManager.instance.Current)
        {
        Scale.value = (int) SelectionManager.instance.Current.transform.localScale.x;
        }
    }
    public static void SetSelectedParameters()  //Sets Current object transform and rotation
    {
        if(SelectionManager.instance.Current)
        {
            Vector3 position = new Vector3(transformX.value, transformY.value, transformZ.value);
            SelectionManager.instance.Current.transform.position = position;
            Quaternion rot = new Quaternion
                (       
                    Rotation.value.x,
                    Rotation.value.y,
                    Rotation.value.z,
                    Rotation.value.w
                );
            SelectionManager.instance.Current.transform.rotation = rot;


            JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, position);  ///save obj data to json
            JsonFileToProject.ProjectFile.SetDataFromRefrence(SelectionManager.instance.Current.name, rot);
        }
    }
    public static void PalleteObjectMenu(string name)
    {
        transformlabel.text = name;
    }
    public static void SetPallete(bool status)
    {
        transformation.SetEnabled(status);
        transformation.value = status;  
    }
}
