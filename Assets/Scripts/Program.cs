using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Program : MonoBehaviour
{
	// Start is called before the first frame update
    void Start()
    {
	    SceneManager.LoadScene("UI_Scene",LoadSceneMode.Additive);
	    print("Loaded UI Scene");
    }


}
