using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCommander : MonoBehaviour
{
	GameObject Owner;
    private void Start()
    {
		Owner = this.gameObject;
		Debug.Log("Hi i am object commander from: " + Owner.name);

	}

    public void Move(Vector3 movement)
	{
		transform.position = transform.position + movement;
	}
}
