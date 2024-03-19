using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///SelectionManager
///<summary>
/// script that interacts with the users scene
/// raycast for selection and movement of objects
/// </summary>

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    public GameObject Current; //object that need commands
    public Stack<GameObject> ObjSelection = new Stack<GameObject>();
    public enum state {inactive,pickup,hold,release }; //set status using mouse left right 
    public state selection = 0;
    Material matref;
    Transform lastselected;
    Vector3 Worldposition = new Vector3();
    Vector3 location = new Vector3();
    MoveableBehaviour mybehaviour;
    void Start()
    {
        instance = this;
        
    }

    void Raycaster()
    {
        if (lastselected != null)
        {
            var LastRenderer = lastselected.GetComponent<Renderer>();
            LastRenderer.material = matref;
            lastselected = null;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            #if UNITY_EDITOR
            Debug.DrawRay(transform.position, ray.direction, Color.green);
            #endif

            var renderer = selection.GetComponent<Renderer>();
            if (renderer != null) //hit sucsses
            {
                matref = renderer.material;
                Current = selection.gameObject;
                if (Current.TryGetComponent<MoveableBehaviour>(out MoveableBehaviour b))
                {
                    renderer.material = Resources.Load<Material>("outlineMaterial");// new Material(Shader.Find("Diffuse"));
                    lastselected = selection;
                }
                else { mybehaviour = Current.AddComponent<MoveableBehaviour>(); }
            }
        }
    }
    void FixedUpdate()
    {
        Raycaster();

        switch (selection)
        {
            case state.pickup:
                {
                    Debug.Log("pickup");
                }
                break;

            case state.hold:
                {
                    Vector3 Screenposition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                    Ray Zray = Camera.main.ScreenPointToRay(Screenposition);
                    if (Physics.Raycast(Zray, out RaycastHit hitdata))
                    {
                        if (Current)
                        {
                            Current.transform.position = new Vector3(hitdata.point.x, hitdata.point.y, Current.transform.position.z); //orignal point of object
                        }
                    }
                }
                break;

            case state.release:
                {
                    Vector3 screen = (Vector3)UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                    screen.z = 0;
                    if (Current)
                    {
                        Debug.Log("Release");
                        
                        IMove a = new IMove(mybehaviour, Worldposition); //is value correct?
                        CommandHandler.ExecuteCommand(a);
                    }
                    selection = state.inactive;
                }
                break;

            default:
                {
                    selection = state.inactive;
                }
                break;
        }
    }
}
