using UnityEngine;

public class MeleeWeapon : Weapon
{
	public override void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		Debug.Log(nameWeapon);
	}

	public override void StopAttack()
	{

	}
}
