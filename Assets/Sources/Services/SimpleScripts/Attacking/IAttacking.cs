using UnityEngine;

public interface IAttacking
{
	public void StartAttack(Transform attackerTransform, Vector3 target);
	public void StopAttack();
}
