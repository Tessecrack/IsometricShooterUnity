using UnityEngine;

public class ShockWeapon : Weapon
{
    protected override void InitWeapon()
    {
        base.InitWeapon();
        nameWeapon = "Shock";
        speed = 50.0f;
        typeWeapon = TypeWeapon.HEAVY;
    }

    public override void StartShoot(ActorController owner, Vector3 targetPosition)
    {
        if (canShoot)
        {
            base.StartShoot(owner, targetPosition);
            canShoot = false;
        }
    }
}
