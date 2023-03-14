using UnityEngine;

public class ShotgunWeapon : Weapon
{
    protected override void InitWeapon()
    {
        nameWeapon = "Shotgun";
        speed = 80.0f;
        delayBetweenShoot = 1.0f;
        typeWeapon = TypeWeapon.HEAVY;
        base.InitWeapon();
    }

    public override void StartShoot(ActorController owner, Vector3 targetPosition)
    {
        if (canShoot && passedTime >= delayBetweenShoot)
        {
            var shotgunShell = 5;
            var spreadShell = Vector3.Distance(targetPosition, owner.transform.position);
            base.StartShoot(owner, targetPosition);
            for (int i = 3; i <= shotgunShell; ++i)
            {
                base.StartShoot(owner, targetPosition + owner.transform.right * spreadShell / i);
                base.StartShoot(owner, targetPosition + owner.transform.right * -spreadShell / i);
            }
            passedTime = 0;
            canShoot = false;
        }
    }
}
