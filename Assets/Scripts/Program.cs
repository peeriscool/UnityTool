using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Program : MonoBehaviour
{
    public bool test;
    UIInputManager UImanager;
    SceneInputManager InputManager = new SceneInputManager();
    private void Awake()
    {
        UImanager = new UIInputManager(GetComponent<UIDocument>());
        InputManager.EnableControls();
        print("Load UI");
      
        //  LoadUI();
    }
    private void Update()
    {
        if(test)
        {
        
        }
    }
    //init UI 
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
        transformlabel.name = "transformvector";
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

        root.AddManipulator(new DragManipulator());
        root.RegisterCallback<DropEvent>(evt =>
          Debug.Log($"{evt.target} dropped on {evt.droppable}"));


        // Get a reference to the field from UXML and assign a value to it.
        var uxmlField = root.Q<Vector3Field>("Vector3Field");
        uxmlField.value = new Vector3(23.8f, 12.6f, 88.3f);

        // Create a new field, disable it, and give it a style class.
        var csharpField = new Vector3Field("C# Field");
        csharpField.SetEnabled(false);
        csharpField.AddToClassList("some-styled-field");
        csharpField.value = uxmlField.value;
        transformation.Add(csharpField);

        // Mirror the value of the UXML field into the C# field.
        uxmlField.RegisterCallback<ChangeEvent<Vector3>>((evt) =>
        {
            csharpField.value = evt.newValue;
        });
    }
    //pass a path to load a json file as a project
    void LoadFile()
    {

    }

}
