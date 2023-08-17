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

	private RangeAttack rangeAttack;

	public override void Init()
	{
		rangeAttack = new RangeAttack(muzzles[0].transform, muzzles[0].transform);
		rangeAttack.SetPrefabProjectile(projectile);
		rangeAttack.SetDamage(damage);
		rangeAttack.SetSpeedProjectile(speedAttack);
	}

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
			rangeAttack.Shot(muzzle.transform, targetPosition);
		}
	}

	protected void ShootFromOneMuzzle(in Transform startTransform, in Vector3 targetPosition)
	{
		var muzzle = muzzles[currentMuzzle++];
		rangeAttack.Shot(muzzle.transform, targetPosition);
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
