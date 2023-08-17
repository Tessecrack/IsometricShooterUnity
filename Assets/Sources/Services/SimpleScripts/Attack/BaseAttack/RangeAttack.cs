using UnityEngine;

public class RangeAttack : IAttack
{
	protected readonly Transform pointSpawn;
	protected readonly Transform owner;

	protected Projectile prefabProjectile;
	protected int damage;
	protected float speedProjectile;

	public bool IsStartAttack { get; protected set; }
	public bool IsAttackInProcess { get; protected set; }
	public RangeAttack(Transform pointSpawn, Transform owner)
	{
		this.pointSpawn = pointSpawn;
		this.owner = owner;
	}

	public void StartAttack()
	{
		IsStartAttack = true;
		IsAttackInProcess = true;
		Shot();
	}

	public void EndAttack()
	{
		IsAttackInProcess = false;
		IsStartAttack = false;
	}

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}

	public void SetPrefabProjectile(Projectile projectile)
	{
		this.prefabProjectile = projectile;
	}

	public void SetSpeedProjectile(float speedProjectile)
	{
		this.speedProjectile = speedProjectile;
	}

	protected void Shot()
	{
		Shot(owner.transform.position + owner.transform.forward * 2);
	}

	public void Shot(Vector3 targetPosition)
	{
		Shot(pointSpawn, targetPosition);
	}

	public void Shot(Transform spawnPointTransform, Vector3 targetPosition)
	{
		var instanceProjectile = Factory.CreateObject<Projectile>(prefabProjectile, spawnPointTransform.position, spawnPointTransform.rotation);
		instanceProjectile.StartFire(owner, targetPosition, speedProjectile, damage);
	}
}
