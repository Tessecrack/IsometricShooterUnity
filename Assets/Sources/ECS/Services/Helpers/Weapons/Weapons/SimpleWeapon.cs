using UnityEngine;

public class SimpleWeapon : Weapon
{
	public override void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		if (canAttack)
		{
			if (quantityOneShotBullet == 1)
			{
				Attack(startTrasform, targetPosition);
			}
			else
			{
				StartCoroutine(GenerateSpreadBullets(startTrasform, targetPosition));
			}
			canAttack = false;
		}
	}

	public override void StopAttack()
	{
		canAttack = true;
	}
}
