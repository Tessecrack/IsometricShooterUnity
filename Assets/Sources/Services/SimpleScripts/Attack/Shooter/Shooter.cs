using UnityEngine;

public abstract class Shooter
{
	protected Transform[] pointsSpawnShots;

	protected Projectile prefabProjectile;

	protected float speedProjectile = 40;

	protected int quantityOneShotProjectile = 1;

	protected int damage;

	public abstract void Shot(Vector3 targetPosition);

	public virtual void Shot()
	{
		foreach (var pointSpawnShot in pointsSpawnShots)
		{
			Shot(pointSpawnShot.position + pointSpawnShot.forward * 2);
		}
	}

	protected virtual void OneShot(Transform spawnPointTransform, Vector3 targetPosition)
	{
		if (spawnPointTransform.position.y != targetPosition.y)
		{
			targetPosition.y = spawnPointTransform.position.y;
		}
		var instanceProjectile = Factory.CreateObject<Projectile>(prefabProjectile, spawnPointTransform.position, spawnPointTransform.rotation);
		instanceProjectile.StartFire(spawnPointTransform, targetPosition, speedProjectile, damage);
	}

	public void SetSpawnPointsShot(in Transform[] spawnPointsTransform)
	{
		this.pointsSpawnShots = spawnPointsTransform;
	}

	public void SetProjectile(in Projectile projectile)
	{
		this.prefabProjectile = projectile;
	}

	public void SetSpeedProjectile(float speedProjectile)
	{
		this.speedProjectile = speedProjectile;
	}

	public void SetQuantityOneShotProjectile(int quantityOneShotProjectile)
	{
		this.quantityOneShotProjectile = quantityOneShotProjectile;
	}

	public void SetDamage(int damage) 
	{ 
		this.damage = damage; 
	}
}
