using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBehaviour : MonoBehaviour
{
	[SerializeField] private LayerMask obstacleLayer;
	//private const float boardSpacing = 1f;

	public void Move(Vector3 movement)
	{
		transform.position = transform.position + movement;
	}
}
