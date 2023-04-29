using UnityEngine;

public class AutomaticWeapon : Weapon
{
	public override void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		if (canAttack)
		{

		}
	}

	public override void StopAttack()
	{
		canAttack = true;
	}
}
