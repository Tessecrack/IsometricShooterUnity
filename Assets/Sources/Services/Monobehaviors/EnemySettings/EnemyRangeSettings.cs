using UnityEngine;

public class EnemyRangeSettings : MonoBehaviour
{
	[Header("FOR ENEMIES WITHOUT ARSENAL")]
	[SerializeField] private Transform[] pointsSpawnProjectile;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private float speedProjectile = 10.0f;
	[SerializeField] private float delayBetweenAttack = 0.5f;

	public float SpeedProjectile => speedProjectile;
	public Transform[] PointsSpawnProjectile => pointsSpawnProjectile;
	public Projectile ProjectilePrefab => projectilePrefab;
	public float DelayBetweenAttack => delayBetweenAttack;
}
