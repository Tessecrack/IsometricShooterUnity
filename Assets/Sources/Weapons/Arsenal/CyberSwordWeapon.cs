namespace Assets.Sources.Weapons.Arsenal
{
	internal class CyberSwordWeapon : Weapon
	{
		protected override void InitWeapon()
		{
			base.InitWeapon();
			nameWeapon = "CyberSwordWeapon";
			speedAttack = 80.0f;
			delayBetweenAttack = 2.0f;
			CurrentTypeWeapon = TypeWeapon.MELEE;
		}
	}
}
