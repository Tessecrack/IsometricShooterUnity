using UnityEngine;

public class GunWeapon : Weapon
{
    protected override void InitWeapon()
    {
        base.InitWeapon();
        nameWeapon = "Gun";
        speedAttack = 100.0f;
        CurrentTypeWeapon = TypeWeapon.GUN;
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
