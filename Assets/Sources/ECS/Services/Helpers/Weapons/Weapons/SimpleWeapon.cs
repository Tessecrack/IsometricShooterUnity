using UnityEngine;

public class SimpleWeapon : Weapon
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
