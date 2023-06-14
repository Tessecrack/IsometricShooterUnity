using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
	[Header("Player prefabs")]
	public GameObject PlayerPrefab;

	[Header("Enemies insectoids prefabs")]
	public StaticEnemies EnemiesInsectoids;

	[Header("Enemy turrets")]
	public StaticEnemyTurret EnemyTurrets;

	[Header("Friendly turrets")]
	public StaticFriendlyTurret FriendlyTurrets;

	[Header("Weapons prefabs")]
	public StaticArsenal Weapons;

	[HideInInspector] public Vector3 GlobalForwardVector { get; private set; } = new Vector3(-1.0f, 0.0f, 1.0f).normalized;
	[HideInInspector] public Vector3 GlobalRightVector { get; private set; } = new Vector3(1.0f, 0.0f, 1.0f).normalized;
	[HideInInspector] public CameraSettings CameraSettings { get; private set; } = new CameraSettings();

	private const int floorLayer = 6;
	public int GetFloorLayer() => floorLayer;
}
