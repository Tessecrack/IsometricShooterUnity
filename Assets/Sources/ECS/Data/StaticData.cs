using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
	public GameObject playerPrefab;
	public float playerSpeed;
	[HideInInspector] public Vector3 GlobalForwardVector { get; private set; } = new Vector3(-1.0f, 0.0f, 1.0f).normalized;
	[HideInInspector] public Vector3 GlobalRightVector { get; private set; } = new Vector3(1.0f, 0.0f, 1.0f).normalized;
	[HideInInspector] public CameraSettings CameraSettings { get; private set; } = new CameraSettings();

	private const int floorLayer = 6;
	public int GetFloorLayer() => floorLayer;
}
