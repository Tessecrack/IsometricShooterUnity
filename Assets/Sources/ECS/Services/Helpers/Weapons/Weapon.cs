using System.Collections;
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

	[SerializeField] protected float delayBetweenAttack = 1.0f;

	protected bool canAttack = true;

	protected int damage = 25;

	protected float speedAttack = 40;


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

	protected IEnumerator GenerateSpreadBullets(Transform ownerTransform, Vector3 targetPosition)
	{
		var partBullets = quantityOneShotBullet / 2;
		for (int i = -partBullets; i <= partBullets; ++i)
		{
			Attack(ownerTransform, targetPosition + i * ownerTransform.right.normalized);
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

	public TypeWeapon GetTypeWeapon() => typeWeapon;

	private void OnDestroy()
	{

	}
}
