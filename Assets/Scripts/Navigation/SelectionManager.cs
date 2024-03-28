using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///SelectionManager
///<summary>
/// script that interacts with the users scene
/// raycast for selection and movement of objects
/// TODO:
/// - Mousescroll for height of the Hold state hitPoint (currently set  to 0)
/// </summary>

public class SelectionManager 
{
    bool Selected = false;
    bool place = false;
    public static SelectionManager instance;
    public GameObject Current; //object that need commands
    public Stack<GameObject> ObjSelection = new Stack<GameObject>();
    public enum state {inactive, Right, hold,release }; //set status using mouse left right 
    public state selection = 0;
    //Material matref;
    Material[] matrefs;
    Transform lastselected;
    private Vector3 m_offset;
    //Vector3 hitPoint = Vector3.zero;
    Plane ground = new Plane();
    public SelectionManager()
    {
        instance = this;
    }

    public void UpdateInput()
    {
        switch (selection)
        {
            case state.hold: //gets called when clicked
                {
                  
                    if (!Selected && Current == null) //whe have nothing selected
                    {
                        Raycaster();
                        place = false;
                    }
                    if(Selected) //second mouse input
                    {
                        place = true;
                        //save position
                        if (Current) JsonFileToProject.ProjectFile.SetDataFromRefrence(Current.name, Current.transform.position);  ///save obj data to json
                    }
                }
                break;

            case state.release:
                {
                    if (Current != null)
                    {
                        Selected = true;
                       
                    }
                        selection = state.inactive;
                }
                break;
            case state.inactive:
                {
                    if(Selected)
                    {
                        //UIController.SetScaleParameters();
                        Ray Zray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            Debug.DrawRay(Zray.origin, Zray.direction, Color.green, 5.0f, true);
                            if (Current && !place)
                            {
                               Vector3 hitPoint = Zray.GetPoint(Vector3.Distance(Camera.main.transform.position, Current.transform.position)); //sets object to mouse raycast position
                                hitPoint.y = 0;
                                Current.transform.position = ExtensionMethods.Round(hitPoint,0) + m_offset;
                            }

                        Program.instance.Uimanager.palleteHandler.UpdateUIParameters(Current.transform);
                    }
                    break;
                }
            case state.Right: //gets called when clicked
                {
                    //deselect object
                    if(Current)
                    {
                        JsonFileToProject.ProjectFile.SetDataFromRefrence(Current.name, Current.transform.position);  ///save obj data to json
                        Program.instance.Uimanager.palleteHandler.UpdateUIParameters(Current.transform);
                    }
                    Current = null;
                    Selected = false;
                    materialcheck();
                    Program.instance.Uimanager.palleteHandler.SetPallete(false);
                }
                break;
        }
    }
    void materialcheck()
    {
        if (lastselected != null)  //get materials and set them in variable, 
        {
            MeshRenderer[] filters = lastselected.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < filters.Length - 1; i++)
            {
                filters[i].material = matrefs[i];
                lastselected.GetChild(i).GetComponent<MeshRenderer>().material = matrefs[i];
            }
            lastselected.GetComponent<MeshRenderer>().material = matrefs[0];
            lastselected = null;
        }
    }
    void Raycaster()
    {
        Debug.Log("raycasting!");
        materialcheck();

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float hitDistance;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            //saves materials of selected object // uses outlineMaterial as selected material
            Renderer[] filters = selection.GetComponentsInChildren<Renderer>();
            matrefs = new Material[filters.Length];
            for (int i = 0; i < filters.Length; i++)
            {
                matrefs[i] = filters[i].material;
                filters[i].material = Resources.Load<Material>("outlineMaterial");
            }
            //also do this for parent
            var renderer = selection.GetComponent<Renderer>();
            //set selected object and make sure we assign materials back afterwards
            if(selection.gameObject)
            {
                Current = selection.gameObject;
                Program.instance.Uimanager.palleteHandler.PalleteObjectMenu(Current.name);
                Program.instance.Uimanager.palleteHandler.SetPallete(true);
                Program.instance.Uimanager.palleteHandler.UpdateUIParameters(Current.transform);
                lastselected = selection;
            }
        }

        if (ground.Raycast(ray, out hitDistance))
        {
            Vector3 hitPoint = ray.GetPoint(hitDistance);
            Current.transform.position = hitPoint;
            m_offset = Current.transform.position - hitPoint;
            //  Current.transform.position = hitPoint + m_offset;
            Debug.Log(hitPoint + "Groundraycast");
        }

    }
}
