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
}
