using UnityEngine;

public class CameraSettings
{
	public Vector3 CameraOffset { get; private set; } = new Vector3(4.0f, 10.0f, -4.0f);
	public float CameraMagnitude { get; private set; } = 3.0f;
	public float AngleCameraX { get; private set; } = 60;
	public float AngleCameraY { get; private set; } = -45;
}
