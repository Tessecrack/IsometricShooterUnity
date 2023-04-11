using System.Collections;
using UnityEngine;

public class FastTurretWeapon : Weapon
{
	private Transform ownerTransform;

	private Vector3 targetPosition;
	protected override void InitWeapon()
	{
		nameWeapon = "FastTurretWeapon";
		speedAttack = 70.0f;
		CurrentTypeWeapon = TypeWeapon.HEAVY;
		DelayBetweenAttack = 0.8f;
		base.InitWeapon();
	}

	public override void StartAttack(Transform ownerTransform, Vector3 targetPosition)
	{
		if (passedAttackTime >= DelayBetweenAttack)
		{
			this.ownerTransform = ownerTransform;
			this.targetPosition = targetPosition;
			StartCoroutine(StartFire());
			passedAttackTime = 0;
		}
	}

	public override void StopAttack()
	{
		canAttack = false;
	}

	IEnumerator StartFire()
	{
		foreach (var muzzle in muzzles)
		{
			var instanceCurrentBullet = Instantiate<Bullet>(bullet, muzzle.transform.position, muzzle.transform.rotation);
			instanceCurrentBullet.StartFire(ownerTransform, targetPosition, speedAttack, damage);
			yield return new WaitForFixedUpdate();
		}
	}
}
