using UnityEngine;

public class MeleeWeapon : Weapon
{
	private MeleeAttack meleeAttack = new MeleeAttack();
	public override void StartAttack(in Transform startTrasform, in Vector3 targetPosition)
	{
		if (!meleeAttack.IsAttackInProcess)
			meleeAttack.StartAttack();
		Debug.Log("YYYY");
	}

	public override void StopAttack()
	{
		if (!meleeAttack.IsAttackInProcess)
			meleeAttack.EndAttack();
	}
}
