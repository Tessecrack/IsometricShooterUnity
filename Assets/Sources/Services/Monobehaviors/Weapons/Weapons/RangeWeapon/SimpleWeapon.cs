using UnityEngine;

public class SimpleWeapon : RangeWeapon
{
	private bool needClickTrigger = false;
	public override void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		if (canAttack && !needClickTrigger)
		{
			if (quantityOneShotBullet == 1)
			{
				Shoot(startTrasform, targetPosition);
			}
			else
			{
				StartCoroutine(GenerateSpreadBullets(startTrasform, targetPosition));
			}
			canAttack = false;
			needClickTrigger = true;
		}
	}

	public override void StopAttack()
	{
		needClickTrigger = false;
	}
}
