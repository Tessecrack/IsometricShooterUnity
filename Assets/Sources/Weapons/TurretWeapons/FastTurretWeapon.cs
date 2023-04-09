using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class FastTurretWeapon : Weapon
{
	private ActorController owner;

	private Vector3 targetPosition;
	protected override void InitWeapon()
	{
		nameWeapon = "FastTurretWeapon";
		speedAttack = 70.0f;
		CurrentTypeWeapon = TypeWeapon.HEAVY;
		DelayBetweenAttack = 0.8f;
		base.InitWeapon();
	}

	public override void StartAttack(ActorController owner, Vector3 targetPosition)
	{
		if (passedAttackTime >= DelayBetweenAttack)
		{
			this.owner = owner;
			this.targetPosition = targetPosition;
			StartCoroutine(StartFire());
			passedAttackTime = 0;
		}
	}

	IEnumerator StartFire()
	{
		foreach (var muzzle in muzzles)
		{
			var instanceCurrentBullet = Instantiate<Bullet>(bullet, muzzle.transform.position, muzzle.transform.rotation);
			instanceCurrentBullet.StartFire(owner, targetPosition, speedAttack, damage);
			yield return new WaitForFixedUpdate();
		}
	}
}
