using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected string nameWeapon;

	[SerializeField] protected TypeWeapon typeWeapon;

	[SerializeField] protected Transform gripWeapon;

	[Header("Only for heavy type weapon")]
	[SerializeField] protected Transform additionalGrip;

	[SerializeField] protected int damage = 25;

	protected bool canAttack = true;

	protected float speedAttack = 40;

	protected float passedTime = 0.0f;
	public abstract void StartAttack(Transform startTrasform, Vector3 targetPosition);

	public abstract void StopAttack();


	public TypeWeapon TypeWeapon => typeWeapon;	
	public Transform AdditionalGrip => additionalGrip;
	public int Damage => damage;

	private void Start()
	{
		if (gripWeapon != null)
		{
			var diff = this.transform.position - this.gripWeapon.position;
			this.transform.position += diff;
		}
	}
}
