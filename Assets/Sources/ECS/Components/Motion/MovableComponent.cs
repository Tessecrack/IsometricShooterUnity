using UnityEngine;

public struct MovableComponent
{
	public Transform transform;

	public float moveSpeed;

	public Vector3 relativeVector;
	public bool isActiveDash;
	public float coefSmooth;

	public bool canMove;
}
