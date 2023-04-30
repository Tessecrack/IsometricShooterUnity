using UnityEngine;

public class AutomaticWeapon : Weapon
{
	private float passedTime = 0.0f;

	private void FixedUpdate()
	{
		if (passedTime >= delayBetweenAttack)
		{
			canAttack = true;
			passedTime = 0.0f;
		}
		if (canAttack == false) 
		{
			passedTime += Time.fixedDeltaTime;
		}		
	}

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
