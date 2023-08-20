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

	private RangeAttack rangeAttack;

	public override void Init()
	{
		Shooter shooter;

		if (quantityOneShotBullet > 1)
		{
			shooter = new SingleShooter();
		}
		else
		{
			shooter = new SpreadShooter();
		}

		rangeAttack = new RangeAttack(shooter);
		BaseAttack = rangeAttack;
	}
}
