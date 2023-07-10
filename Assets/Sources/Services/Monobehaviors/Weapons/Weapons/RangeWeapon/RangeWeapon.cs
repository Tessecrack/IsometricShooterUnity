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

	[SerializeField] protected float delayBetweenAttack = 1.0f;

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

	protected void Attack(Transform startTransform, Vector3 targetPosition)
	{
		if (isAttackFromOneMuzzle)
		{
			AttackFromOneMuzzle(startTransform, targetPosition);
			return;
		}

		foreach (var muzzle in muzzles)
		{
			var instanceProjectile = Instantiate<Projectile>(projectile, muzzle.transform.position, muzzle.transform.rotation);
			instanceProjectile.StartFire(startTransform, targetPosition, speedAttack, damage);
		}
	}

	protected void AttackFromOneMuzzle(Transform startTransform, Vector3 targetPosition)
	{
		var muzzle = muzzles[currentMuzzle++];
		var instanceProjectile = Instantiate<Projectile>(projectile, muzzle.transform.position, muzzle.transform.rotation);
		instanceProjectile.StartFire(startTransform, targetPosition, speedAttack, damage);
		currentMuzzle %= muzzles.Count;
	}

	protected IEnumerator GenerateSpreadBullets(Transform ownerTransform, Vector3 targetPosition)
	{
		var partBullets = quantityOneShotBullet / 2;
		for (int i = -partBullets; i <= partBullets; ++i)
		{
			Attack(ownerTransform, targetPosition + i * ownerTransform.right.normalized);
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

}
