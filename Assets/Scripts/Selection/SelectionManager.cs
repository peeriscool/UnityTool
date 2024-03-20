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

public class SelectionManager : MonoBehaviour
{
    bool Selected = false;
    public static SelectionManager instance;
    public GameObject Current; //object that need commands
    public Stack<GameObject> ObjSelection = new Stack<GameObject>();
    public enum state {inactive,pickup,hold,release }; //set status using mouse left right 
    public state selection = 0;
    Material matref;
    Material[] matrefs;
    Transform lastselected;
    Vector3 Worldposition = new Vector3();
    Vector3 location = new Vector3();
    private Vector3 m_offset;
    Vector3 hitPoint = Vector3.zero;

    MoveableBehaviour mybehaviour;
    void Start()
    {
        instance = this;
        
    }

    void Raycaster()
    {
        if (lastselected != null)  //get materials and set them in variable, 
        {
            MeshRenderer[] filters = lastselected.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < filters.Length-1; i++)
            {
                filters[i].material = matrefs[i];
                lastselected.GetChild(i).GetComponent<MeshRenderer>().material = matrefs[i];
            }
            lastselected.GetComponent<MeshRenderer>().material = matrefs[0];
           lastselected = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float hitDistance;
        Plane ground = new Plane();

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, ray.direction, Color.green);
            #endif  

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
            matref = renderer.material;


            Current = selection.gameObject;
            lastselected = selection;
        }
       
      
        if (ground.Raycast(ray, out hitDistance))
        {
            Vector3 hitPoint = ray.GetPoint(hitDistance);
            Current.transform.position = hitPoint;
            m_offset = Current.transform.position - hitPoint;
            Current.transform.position = hitPoint + m_offset;
        }


    }
    void FixedUpdate()
    {


        switch (selection)
        {
            case state.hold: //gets called when clicked
                {
                   
                    if (Selected && Current) //reset selected if current raycast    
                    {
                        Current = null;
                        //whe are holding an object place on new location
                        Selected = false;
                    }
                 
                  
                        // Vector3 Screenposition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                      //  Ray Zray = Camera.main.ScreenPointToRay(Input.mousePosition);
                     //   if (Physics.Raycast(Zray, out RaycastHit hitdata))
                     //   {
                     //       hitPoint = Zray.GetPoint(hitdata.distance);
                     //       if (Current)
                      //      {

                       //         hitPoint.y = 0;
                                // Current.transform.position = hitPoint + m_offset;
                      //          Selected = true;
                                //   Current.transform.position = new Vector3(hitdata.point.x, hitdata.point.y, Current.transform.position.z); //orignal point of object
                       //     }
                    //   }
                    
                    if (!Selected && Current == null) //whe have nothing selected
                    {
                        Raycaster();
                    }
                    
                    //if (clicktoggle)
                    //{
                    //Debug.Log(clicktoggle);
                    //Current.transform.position = hitPoint + m_offset;

                    //}

                }
                break;

            case state.release:
                {
                    if (Current != null)
                    {
                        Selected = true;
                    }
                        selection = state.inactive;
                    //Vector3 screen = (Vector3)UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                    //screen.z = 0;
                    //if (Current )
                    //{
                    //    Selected = false;
                    //    Debug.Log("Release");
                    //   // ClickToggle();
                    //    //IMove a = new IMove(mybehaviour, Worldposition); //is value correct?
                    //    //CommandHandler.ExecuteCommand(a);
                    //}
                    //selection = state.inactive;
                }
                break;
            case state.inactive:
                {
                    if(Selected)
                    {
                        Ray Zray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(Zray, out RaycastHit hitdata))
                        {
                            hitPoint = Zray.GetPoint(hitdata.distance);
                            if (Current)
                            {

                                hitPoint.y = 0;
                                Current.transform.position = hitPoint + m_offset;
                                //   Current.transform.position = new Vector3(hitdata.point.x, hitdata.point.y, Current.transform.position.z); //orignal point of object
                            }
                        }
                    }
                    else if(Current)
                    { //we want to place object
                        Debug.Log("when mouse is pressed");
                    }

                  
                    //if (clicktoggle && Current != null)
                    //{
                    //    Debug.Log(clicktoggle);
                    //    Current.transform.position = hitPoint + m_offset;

                    //}
                    break;
                }
            //default:
            //    {
            //        selection = state.inactive;
            //    }
            //    break;
        }
    }
   
}
