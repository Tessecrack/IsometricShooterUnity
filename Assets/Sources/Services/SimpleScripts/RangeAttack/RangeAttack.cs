using Unity.VisualScripting;
using UnityEngine;

public class RangeAttack
{
	private Transform pointSpawn;
	private Transform owner;

	private Projectile prefabProjectile;

	private bool isAttackInProcess;
	private int damage;
	private float speedProjectile;

	public RangeAttack(AnimationEvents animationEvents, Transform pointSpawn, Projectile prefabProjectile)
	{
		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;
		animationEvents.OnShot += Shot;

		this.pointSpawn = pointSpawn;
		this.prefabProjectile = prefabProjectile;
	}

	public void StartAttack()
	{
		isAttackInProcess = true;
	}

	public void EndAttack()
	{
		isAttackInProcess = false;
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

	public void Shot()
	{
		var instanceProjectile = Object.Instantiate<Projectile>(prefabProjectile, 
			pointSpawn.transform.position, pointSpawn.transform.rotation);

		instanceProjectile.StartFire(owner,
			owner.transform.position + owner.transform.forward * 2, speedProjectile, damage);
	}

	public bool AttackInProcess => isAttackInProcess;
}
