using UnityEngine;

public class ArchtronicWeapon : Weapon
{
    protected override void InitWeapon()
    {
        base.InitWeapon();
        nameWeapon = "Archtronic";
        speed = 70.0f;
        typeWeapon = TypeWeapon.HEAVY;
        delayBetweenShoot = 0.1f;
    }

    public override void StartShoot(ActorController owner, Vector3 targetPosition)
    {
        if (passedTime >= delayBetweenShoot)
        {
            base.StartShoot(owner, targetPosition);
            passedTime = 0;
        }
        
    }
}
