using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public TypeWeapon CurrentTypeWeapon { get; protected set; }
    public float DelayBetweenAttack { get; protected set; }

	[SerializeField] protected List<GameObject> muzzles;

    [SerializeField] protected Bullet bullet;

	public string nameWeapon;

    protected float speedAttack = 10.0f;

    protected int damage = 10;

    protected bool canAttack;

    protected float passedAttackTime;

    public abstract void StartAttack(Transform ownerTransform, Vector3 targetPosition);
    public abstract void StopAttack();

	private void Awake()
	{
        InitWeapon();
	}

    private void FixedUpdate()
    {
        passedAttackTime += Time.fixedDeltaTime;
    }

    protected void SpawnBullet(Transform ownerTransform, Vector3 targetPosition)
    {
        if (CurrentTypeWeapon == TypeWeapon.MELEE)
        {
            return;
        }
        foreach (var muzzle in muzzles)
        {
            var instanceCurrentBullet = Instantiate<Bullet>(bullet, muzzle.transform.position, muzzle.transform.rotation);
            instanceCurrentBullet.StartFire(ownerTransform, targetPosition, speedAttack, damage);
        }
    }

    protected virtual void InitWeapon() 
    {
        passedAttackTime = DelayBetweenAttack;
        canAttack = true;
    }
}
