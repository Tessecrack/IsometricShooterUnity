using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutomaticWeapon : RangeWeapon
{
	public override void Init()
	{
		Shooter shooter;
		if (quantityOneShotBullet == 1)
		{
			shooter = new SingleShooter();
		}
		else
		{
			shooter = new SpreadShooter();
		}

		shooter.SetProjectile(projectile);
		shooter.SetQuantityOneShotProjectile(quantityOneShotBullet);
		shooter.SetSpawnPointsShot(muzzles.Select(m => m.transform).ToArray());
		shooter.SetSpeedProjectile(speedAttack);
		shooter.SetDamage(damage);

		BaseAttack = new RangeAutomaticAttack(shooter);
	}
}
