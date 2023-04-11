using UnityEngine;

namespace Assets.Sources.Weapons.Arsenal
{
	internal class CyberSwordWeapon : Weapon
	{
		protected override void InitWeapon()
		{
			base.InitWeapon();
			nameWeapon = "CyberSwordWeapon";
			speedAttack = 80.0f;
			DelayBetweenAttack = 1.0f;
			CurrentTypeWeapon = TypeWeapon.MELEE;
		}

		public override void StartAttack(Transform ownerTransform, Vector3 targetPosition)
		{
			if (canAttack && passedAttackTime >= DelayBetweenAttack)
			{
				passedAttackTime = 0;
				canAttack = false;
			}
		}

		public override void StopAttack()
		{
			canAttack = true;
		}
	}
}
