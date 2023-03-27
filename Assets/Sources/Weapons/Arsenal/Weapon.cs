using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject muzzle;

    [SerializeField] protected Bullet bullet;

	public string nameWeapon;

    protected float speedAttack = 10.0f;

    protected float damage = 10.0f;

    protected bool canAttack;

    protected float delayBetweenAttack;

    protected float passedAttackTime;
    public TypeWeapon CurrentTypeWeapon { get; protected set; } = TypeWeapon.GUN; // it's seems strange, but all my weapons without this will be MELEE on start :)

    private void Start()
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
        passedAttackTime = delayBetweenAttack;
        canAttack = true;
    }
}
