using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public TypeWeapon CurrentTypeWeapon { get; protected set; }
    public float DelayBetweenAttack { get; protected set; }

	[SerializeField] protected GameObject muzzle;

    [SerializeField] protected Bullet bullet;

	public string nameWeapon;

    protected float speedAttack = 10.0f;

    protected float damage = 10.0f;

    protected bool canAttack;

    protected float passedAttackTime;

	private void Awake()
	{
        InitWeapon();
	}

    private void FixedUpdate()
    {
        passedAttackTime += Time.fixedDeltaTime;
    }

    public virtual void StartAttack(ActorController owner, Vector3 targetPosition)
    {
        if (CurrentTypeWeapon == TypeWeapon.MELEE)
        {
            return;
        }
        var instanceCurrentBullet = Instantiate<Bullet>(bullet, muzzle.transform.position, muzzle.transform.rotation);
        instanceCurrentBullet.StartFire(owner, targetPosition, speedAttack, damage);
    }

    public virtual void StopAttack()
    {
        canAttack = true;
    }

    protected virtual void InitWeapon() 
    {
        passedAttackTime = DelayBetweenAttack;
        canAttack = true;
    }
}
