using UnityEngine;

public class FastTurretWeapon : Weapon
{
	protected override void InitWeapon()
	{
		nameWeapon = "FastTurretWeapon";
		speedAttack = 70.0f;
		CurrentTypeWeapon = TypeWeapon.HEAVY;
		DelayBetweenAttack = 0.4f;
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
