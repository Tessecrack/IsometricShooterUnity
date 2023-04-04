using System.Collections;
using System.Collections.Generic;
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

	public override void StartAttack(ActorController owner, Vector3 targetPosition)
	{
		if (passedAttackTime >= DelayBetweenAttack)
		{
			base.StartAttack(owner, targetPosition);
			passedAttackTime = 0;
		}

	}
}
