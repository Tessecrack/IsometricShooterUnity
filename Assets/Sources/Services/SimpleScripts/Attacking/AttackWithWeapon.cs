using UnityEngine;

public class AttackWithWeapon : IAttacking
{
	public void StartAttack(Transform attackerTransform, Vector3 target)
	{
		UnityEngine.Debug.Log("START ATTACK WITH WEAPON");
	}

	public void StopAttack()
	{
		UnityEngine.Debug.Log("STOP ATTACK WITH WEAPON");
	}
}
