using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected string nameWeapon;

	[SerializeField] protected TypeWeapon typeWeapon;

	[SerializeField] protected Transform gripWeapon;

	[SerializeField] protected int damage = 25;

	[Header("Only for heavy type weapon")]
	[SerializeField] protected Transform additionalGrip;

	protected bool canAttack = true;

	protected float speedAttack = 40;

	protected float passedTime = 0.0f;

	public BaseAttack BaseAttack { get; protected set; }

	public TypeWeapon TypeWeapon => typeWeapon;	
	public int Damage => damage;

	public virtual void Init()
	{

	}

	public void SetGripWeapon()
	{
		if (gripWeapon != null)
		{
			var diff = this.transform.position - this.gripWeapon.position;
			this.transform.position += diff;
		}
	}

	public Transform AdditionalGrip => additionalGrip;
}
