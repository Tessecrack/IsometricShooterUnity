using System.Collections.Generic;
using UnityEngine;

public class RuntimeData
{
	public byte MaxCountWeapons { get; private set; } = 3;

	public Vector3 OwnerCameraTransform { get; set; }

	public Vector3 CursorPosition { get; set; }

	public Vector3 PlayerPosition { get; set; }

	public void SetCursorPosition(Vector3 cursorPosition)
	{
		CursorPosition = cursorPosition;
	}

	public Vector3 GetModifyCursorPosition() => new Vector3(CursorPosition.x, OwnerCameraTransform.y, CursorPosition.z);

	public bool IsPlayerDeath { get; set; }
}
