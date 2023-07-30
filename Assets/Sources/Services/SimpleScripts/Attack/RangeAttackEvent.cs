using UnityEngine;

public class RangeAttackEvent : AttackEvent
{
	private Transform pointSpawn;
	private Transform owner;

	private Projectile prefabProjectile;
	private int damage;
	private float speedProjectile;

	public RangeAttackEvent(AnimationEvents animationEvents, Transform pointSpawn, Projectile prefabProjectile) : base(animationEvents)
	{
		animationEvents.OnShot += Shot;

		TypeAttack = TypeAttack.Range;

		this.pointSpawn = pointSpawn;
		this.prefabProjectile = prefabProjectile;
	}

	public void SetOwner(Transform owner)
	{
		this.owner = owner;
	}

	public void SetDamage(int damage)
	{
		this.damage = damage;
	}

	public void SetSpeedProjectile(float speedProjectile)
	{
		this.speedProjectile = speedProjectile;
	}

	private void Shot()
	{
		var instanceProjectile = Object.Instantiate<Projectile>(prefabProjectile, 
			pointSpawn.transform.position, pointSpawn.transform.rotation);

		instanceProjectile.StartFire(owner,
			owner.transform.position + owner.transform.forward * 2, speedProjectile, damage);
	}
}
