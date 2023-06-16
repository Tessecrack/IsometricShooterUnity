using UnityEngine;

public class EnemySettings : MonoBehaviour
{
	[SerializeField] private TypeEnemy typeEnemy;

	[SerializeField] private bool needChargeWeapon;

	public TypeEnemy TypeEnemy => typeEnemy;

	public bool NeedChargeWeapon => needChargeWeapon;
}
