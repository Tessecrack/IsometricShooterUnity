using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
	public GameObject playerPrefab;
	public float playerSpeed;
	[HideInInspector] public Vector3 GlobalForwardVector { get; private set; } = new Vector3(-1.0f, 0.0f, 1.0f).normalized;
	[HideInInspector] public Vector3 GlobalRightVector { get; private set; } = new Vector3(1.0f, 0.0f, 1.0f).normalized;

	private const float angleCameraX = 60;
	private const float angleCameraY = -45;
	public float GetAngleCameraX() => angleCameraX;
	public float GetAngleCameraY() => angleCameraY;
}
