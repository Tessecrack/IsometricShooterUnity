using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject muzzle;

    [SerializeField] protected Bullet bullet;

    protected string nameWeapon;

    protected float speed = 10.0f;

    protected float damage = 10.0f;

    protected bool canShoot;

    protected float delayBetweenShoot;

    protected float passedTime;

    public TypeWeapon typeWeapon { get; protected set; }

    private void Start()
    {
        InitWeapon();
    }

    private void FixedUpdate()
    {
        passedTime += Time.fixedDeltaTime;
    }

    public virtual void StartShoot(ActorController owner, Vector3 targetPosition)
    {
        var instanceCurrentBullet = Instantiate<Bullet>(bullet, muzzle.transform.position, muzzle.transform.rotation);
        instanceCurrentBullet.StartFire(owner, targetPosition, speed, damage);
    }

    public virtual void StopShoot()
    {
        canShoot = true;
    }

    protected virtual void InitWeapon() 
    {
        passedTime = delayBetweenShoot;
        canShoot = true;
    }
}
