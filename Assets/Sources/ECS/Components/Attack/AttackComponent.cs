using UnityEngine;

public struct AttackComponent
{
	public Transform attackerTransform;
	public Vector3 targetPoint;

	public bool isStartAttack;
	public bool isStopAttack;

	public TypeAttack typeAttack;
}
