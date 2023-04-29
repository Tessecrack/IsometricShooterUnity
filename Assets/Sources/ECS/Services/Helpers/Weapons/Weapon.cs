using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected string nameWeapon;

	[SerializeField] protected TypeWeapon typeWeapon;

	[SerializeField] protected GameObject prefabProjectTile;

	[SerializeField] protected List<GameObject> muzzles;

	[SerializeField] protected int quantityOneShotBullet = 1;

	[SerializeField] protected Projectile projectile;

	protected int lifeTime = 3;

	protected bool canAttack = true;

	protected float delayBetweenAttack = 2.0f;

	protected int damage = 25;

	protected float speedAttack = 20;

	public abstract void StartAttack(Transform startTrasform, Vector3 targetPosition);

	public abstract void StopAttack();

	protected void Attack(Transform startTransform, Vector3 targetPosition)
	{
		foreach (var muzzle in muzzles)
		{
			var instanceProjectile = Instantiate<Projectile>(projectile, muzzle.transform.position, muzzle.transform.rotation);
			instanceProjectile.StartFire(startTransform, targetPosition, speedAttack, damage);
		}
	}

	public TypeWeapon GetTypeWeapon() => typeWeapon;

	private void OnDestroy()
	{
		
	}
}
