using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Program : MonoBehaviour
{
	int num = 60;
	// Start is called before the first frame update
    void Start()
    {
	    SceneManager.LoadScene("UI_Scene",LoadSceneMode.Additive);
	    print("Loaded UI Scene");
    }

    // Update is called once per frame
    void Update()
	{
		/*
		print(UnityEngine.Time.frameCount); //frame number
		for (int i = num; i == UnityEngine.Time.frameCount;) {
			num=+60;
			print(num);	
			break;
		}
		*/
    }
}
