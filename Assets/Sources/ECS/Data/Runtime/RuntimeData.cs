using System.Collections.Generic;
using UnityEngine;

public class RuntimeData
{
	public List<byte> CurrentArsenal = new List<byte>();

	public byte MaxCountWeapons { get; private set; } = 3;

	public Transform OwnerCameraTransform { get; set; }

	public Vector3 CursorPosition { get; set; }

	public Vector3 PlayerPosition { get; set; }

	public void SetCursorPosition(Vector3 cursorPosition)
	{
		CursorPosition = cursorPosition;
	}

	public Vector3 GetModifyCursorPosition() => new Vector3(CursorPosition.x, OwnerCameraTransform.position.y, CursorPosition.z);
}
