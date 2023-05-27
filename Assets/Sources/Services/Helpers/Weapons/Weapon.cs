using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected string nameWeapon;

	[SerializeField] protected TypeWeapon typeWeapon;

	[SerializeField] protected float delayBetweenAttack = 1.0f;

	[SerializeField] protected Transform gripWeapon;

	[SerializeField] protected Transform additionalGrip; // only for HEAVY weapon

	protected bool canAttack = true;

	protected int damage = 25;

	protected float speedAttack = 40;

	protected float passedTime = 0.0f;

	public abstract void StartAttack(Transform startTrasform, Vector3 targetPosition);

	public abstract void StopAttack();
	public TypeWeapon GetTypeWeapon() => typeWeapon;

	public Transform GetGripWeapon() => gripWeapon;

	public Transform GetAdditionalGrip() => additionalGrip;

	private void Start()
	{
		if (gripWeapon != null)
		{
			var diff = this.transform.position - this.gripWeapon.position;
			this.transform.position += diff;
		}
	}
}