using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Sources.Weapons.Arsenal
{
	internal class CyberSwordWeapon : Weapon
	{
		protected override void InitWeapon()
		{
			base.InitWeapon();
			nameWeapon = "CyberSwordWeapon";
			speedAttack = 80.0f;
			delayBetweenAttack = 1.0f;
			CurrentTypeWeapon = TypeWeapon.MELEE;
		}
	}
}
