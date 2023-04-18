using UnityEngine;

public class CameraSettings
{
	public Vector3 CameraOffset { get; private set; } = new Vector3(3.0f, 10.0f, -3.0f);
	public float CameraMagnitude { get; private set; } = 3.0f;

	private const float angleCameraX = 60;
	private const float angleCameraY = -45;

	public float GetAngleCameraX() => angleCameraX;
	public float GetAngleCameraY() => angleCameraY;
}
