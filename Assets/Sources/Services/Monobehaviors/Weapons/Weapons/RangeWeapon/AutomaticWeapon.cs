using UnityEngine;

public class AutomaticWeapon : RangeWeapon
{
	public override void StartAttack(in Transform startTrasform, in Vector3 targetPosition)
	{
		if (canAttack)
		{
			Shoot(startTrasform, targetPosition);
			canAttack = false;
		}
	}

	public override void StopAttack()
	{
		canAttack = true;
	}
}
