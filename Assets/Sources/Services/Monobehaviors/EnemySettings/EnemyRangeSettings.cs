using UnityEngine;

public class EnemyRangeSettings : MonoBehaviour
{
	[Header("FOR ENEMIES WITHOUT ARSENAL")]
	[SerializeField] private Transform pointSpawnProjectile;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private float speedProjectile;

	public float SpeedProjectile => speedProjectile;
	public Transform PointSpawnProjectile => pointSpawnProjectile;
	public Projectile ProjectilePrefab => projectilePrefab;
}
