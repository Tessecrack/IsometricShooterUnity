using UnityEngine;

public class MaulerWeapon : Weapon
{
	protected override void InitWeapon()
	{
		nameWeapon = "Mauler";
		speedAttack = 70.0f;
		CurrentTypeWeapon = TypeWeapon.HEAVY;
		DelayBetweenAttack = 0.1f;
		base.InitWeapon();
	}

	public override void StartAttack(Transform ownerTransform, Vector3 targetPosition)
	{
		if (passedAttackTime >= DelayBetweenAttack)
		{
			SpawnBullet(ownerTransform, targetPosition);
			passedAttackTime = 0;
		}

	}

	public override void StopAttack()
	{
		canAttack = true;
	}
}
