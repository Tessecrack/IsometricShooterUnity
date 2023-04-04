using UnityEngine;

public class HellWailerWeapon : Weapon
{
	protected override void InitWeapon()
	{
		nameWeapon = "Hellwailer";
		speedAttack = 70.0f;
		CurrentTypeWeapon = TypeWeapon.HEAVY;
		DelayBetweenAttack = 0.5f;
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
