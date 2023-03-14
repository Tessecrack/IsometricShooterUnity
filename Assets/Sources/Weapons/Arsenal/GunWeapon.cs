using UnityEngine;

public class GunWeapon : Weapon
{
    protected override void InitWeapon()
    {
        base.InitWeapon();
        nameWeapon = "Gun";
        speed = 100.0f;
        typeWeapon = TypeWeapon.GUN;
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
