using UnityEngine;

public class EnemyRangeSettings : MonoBehaviour
{
	[Header("FOR ENEMIES WITHOUT ARSENAL")]
	[SerializeField] private Transform[] pointsSpawnProjectile;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private float speedProjectile;

	public float SpeedProjectile => speedProjectile;
	public Transform[] PointsSpawnProjectile => pointsSpawnProjectile;
	public Projectile ProjectilePrefab => projectilePrefab;
}
