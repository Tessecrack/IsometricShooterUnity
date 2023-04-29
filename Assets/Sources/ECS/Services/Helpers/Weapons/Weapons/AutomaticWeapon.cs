using UnityEngine;

public class AutomaticWeapon : Weapon
{
	public override void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		Debug.Log(nameWeapon);
	}

	public override void StopAttack()
	{
		
	}
}
