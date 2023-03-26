using UnityEngine;

public class ShockWeapon : Weapon
{
    protected override void InitWeapon()
    {
        base.InitWeapon();
        nameWeapon = "Shock";
        speedAttack = 50.0f;
        CurrentTypeWeapon = TypeWeapon.HEAVY;
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
