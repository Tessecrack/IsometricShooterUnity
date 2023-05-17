using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected string nameWeapon;

	[SerializeField] protected TypeWeapon typeWeapon;

	[SerializeField] protected float delayBetweenAttack = 1.0f;

	protected bool canAttack = true;

	protected int damage = 25;

	protected float speedAttack = 40;

	protected float passedTime = 0.0f;

	public abstract void StartAttack(Transform startTrasform, Vector3 targetPosition);

	public abstract void StopAttack();
	public TypeWeapon GetTypeWeapon() => typeWeapon;
}
