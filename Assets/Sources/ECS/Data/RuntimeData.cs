using UnityEngine;

public class RuntimeData
{
	public int MaxCountWeapons { get; private set; } = 3;

	public Transform OwnerCameraTransform { get; set; }

	public Vector3 CursorPosition { get; set; }
}
