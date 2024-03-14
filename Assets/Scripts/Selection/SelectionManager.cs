using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    public GameObject Selected; //object that need commands
    public enum state {inactive,pickup,hold,release }; //set status using mouse left right 
    public state selection = 0;
    Material matref;
    Transform lastselected;
    Vector3 Worldposition = new Vector3();
    Vector3 location = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
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
                Selected = selection.gameObject;
                renderer.material = new Material(Shader.Find("Diffuse"));
                lastselected = selection;
            }
        }
        switch (selection)
        {
            case state.pickup:
                {
                    Debug.Log("pickup");
                  
                        Vector3 Screenposition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                        Ray Zray = Camera.main.ScreenPointToRay(Screenposition);
                        if (Physics.Raycast(Zray, out RaycastHit hitdata))
                        {
                            Worldposition = hitdata.point;
                        }
                        //  Screenposition.z = Camera.main.nearClipPlane + 1f;
                }
                break;
            case state.hold:
                {
                    Vector3 screen = (Vector3)UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                    screen.z = 0;
                    if (Selected) { Selected.transform.position = Vector3.Lerp(Worldposition,screen,3f) /100; }//Vector3.Lerp(Worldposition ,(Vector3)UnityEngine.InputSystem.Mouse.current.position.ReadValue(),3f) /100; }    
                    //update while holding
                }
                break;
            case state.release:
                {
                    Debug.Log("Release");
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
