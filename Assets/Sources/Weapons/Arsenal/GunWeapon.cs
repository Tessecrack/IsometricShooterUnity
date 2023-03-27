using UnityEngine;

public class GunWeapon : Weapon
{
	protected override void InitWeapon()
    {
		CurrentTypeWeapon = TypeWeapon.GUN;
		nameWeapon = "Gun";
		speedAttack = 100.0f;
		base.InitWeapon();
    }

    public override void StartAttack(ActorController owner, Vector3 targetPosition)
    {
        if (canAttack)
        {
            base.StartAttack(owner, targetPosition);
            canAttack = false;
        }
    }
}
