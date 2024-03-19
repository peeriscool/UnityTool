using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using AnotherFileBrowser.Windows;

//loads a UIDocument to the scene
class UIController : MonoBehaviour
    {
    UIDocument Mydocument;
    UIController()
    {
    }
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
            //History

            //create buttons
            Slider Scale = new Slider();
            Scale.name = "scalevector";
            Scale.label = "Scale";
            // Scale.value = Vector3.one;
            Scale.style.fontSize = 10;

            //Vector3Field transform = new Vector3Field();
            //transform.name = "transformvector";
            //transform.label = "transform";
            //transform.value = Vector3.zero;
            //transform.style.fontSize = 10;

            Label transformlabel = new Label();
            transformlabel.style.fontSize = 10;
            transformlabel.name = "transform vector";
            IntegerField transformX = new IntegerField();
            IntegerField transformY = new IntegerField();
            IntegerField transformZ = new IntegerField();

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

            // transformX.ElementAt(0).style
            Vector4Field Rotation = new Vector4Field();
            Rotation.name = "rotatevector";
            Rotation.label = "rotate";
            Rotation.value = new Vector4();
            Rotation.style.fontSize = 10;
            //Pallete
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            Foldout transformation = root.Q<Foldout>("Translation");

            transformation.Add(Scale);
            transformation.Add(transformlabel);
            transformation.Add(transformX);
            transformation.Add(transformY);
            transformation.Add(transformZ);
            transformation.Add(Rotation);
            Button Exportbutton = root.Q<Button>("Export");
            Button Savebutton = root.Q<Button>("SaveBut");
            Exportbutton.clicked += ExportEvent;
            Savebutton.clicked += SaveEvent;
        //root.AddManipulator(new DragManipulator());
        //root.RegisterCallback<DropEvent>(evt =>
        //      Debug.Log($"{evt.target} dropped on {evt.droppable}"));


        //// Get a reference to the field from UXML and assign a value to it.
        //var uxmlField = root.Q<Vector3Field>("Vector3Field");
        //uxmlField.value = new Vector3(23.8f, 12.6f, 88.3f);

        //// Create a new field, disable it, and give it a style class.
        //var csharpField = new Vector3Field("C# Field");
        //csharpField.SetEnabled(false);
        //csharpField.AddToClassList("some-styled-field");
        //csharpField.value = uxmlField.value;
        //transformation.Add(csharpField);

        //// Mirror the value of the UXML field into the C# field.
        //uxmlField.RegisterCallback<ChangeEvent<Vector3>>((evt) =>
        //{
        //    csharpField.value = evt.newValue;
        //});
        }
    private void SaveEvent()
    {
        //temp savedata
        ProjectData data1 = new ProjectData();
        data1.ProjectName = "ProjectFile_01";
        data1.Version = 0.1f;

        JSONSerializer.Save(data1.ProjectName,data1);
        Debug.Log("Control if Data exists");
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
        bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //   Load Binary or Json format of project

            Debug.Log(path);
            Debug.Log("Load Binary or Json format of project");
            sendpath = path;

        });
        return sendpath;
    }
}
