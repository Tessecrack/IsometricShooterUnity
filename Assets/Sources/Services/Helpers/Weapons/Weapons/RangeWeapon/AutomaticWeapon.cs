using UnityEngine;

public class AutomaticWeapon : RangeWeapon
{
	public override void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		if (canAttack)
		{
			Attack(startTrasform, targetPosition);
			canAttack = false;
		}
	}

	public override void StopAttack()
	{
		canAttack = true;
	}
}
