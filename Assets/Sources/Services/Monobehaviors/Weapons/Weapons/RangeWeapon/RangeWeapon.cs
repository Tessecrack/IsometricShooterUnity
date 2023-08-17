using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
	[SerializeField] protected GameObject prefabProjectTile;

	[SerializeField] protected List<GameObject> muzzles;

	[SerializeField] protected int quantityOneShotBullet = 1;

	[SerializeField] protected Projectile projectile;

	[SerializeField] protected bool isAttackFromOneMuzzle = false;

	[SerializeField] protected float delayBetweenAttack = 0.05f;

	private int currentMuzzle = 0;

	private void FixedUpdate()
	{
		if (passedTime >= delayBetweenAttack)
		{
			canAttack = true;
			passedTime = 0.0f;
		}
		if (canAttack == false)
		{
			passedTime += Time.fixedDeltaTime;
		}
	}

	protected void Shoot(in Transform startTransform, in Vector3 targetPosition)
	{
		if (isAttackFromOneMuzzle)
		{
			ShootFromOneMuzzle(startTransform, targetPosition);
			return;
		}

		foreach (var muzzle in muzzles)
		{
			var instanceProjectile = Factory.CreateObject<Projectile>(projectile, muzzle.transform.position, muzzle.transform.rotation);
			instanceProjectile.StartFire(startTransform, targetPosition, speedAttack, damage);
		}
	}

	protected void ShootFromOneMuzzle(in Transform startTransform, in Vector3 targetPosition)
	{
		var muzzle = muzzles[currentMuzzle++];
		var instanceProjectile = Factory.CreateObject<Projectile>(projectile, muzzle.transform.position, muzzle.transform.rotation);
		instanceProjectile.StartFire(startTransform, targetPosition, speedAttack, damage);
		currentMuzzle %= muzzles.Count;
	}

	protected IEnumerator GenerateSpreadBullets(Transform ownerTransform, Vector3 targetPosition)
	{
		var partBullets = quantityOneShotBullet / 2;
		for (int i = -partBullets; i <= partBullets; ++i)
		{
			Shoot(ownerTransform, targetPosition + i * ownerTransform.right.normalized);
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}
}
