using UnityEngine;

public class ArchtronicWeapon : Weapon
{
    protected override void InitWeapon()
    {
        nameWeapon = "Archtronic";
        speedAttack = 70.0f;
        CurrentTypeWeapon = TypeWeapon.HEAVY;
        delayBetweenAttack = 0.1f;
		base.InitWeapon();
	}

    public override void StartAttack(ActorController owner, Vector3 targetPosition)
    {
        if (passedAttackTime >= delayBetweenAttack)
        {
            base.StartAttack(owner, targetPosition);
            passedAttackTime = 0;
        }
        
    }
}
