using UnityEngine;

public class AttackWithoutWeapon : IAttacking
{
	public void StartAttack(Transform attackerTransform, Vector3 target)
	{
		UnityEngine.Debug.Log("START ATTACK WITHOUT WEAPON");
	}

	public void StopAttack()
	{
		UnityEngine.Debug.Log("STOP ATTACK WITHOUT WEAPON");
	}
}
