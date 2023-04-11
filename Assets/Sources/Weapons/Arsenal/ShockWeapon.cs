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

    public override void StartAttack(Transform ownerTransform, Vector3 targetPosition)
    {
        if (canAttack)
        {
            SpawnBullet(ownerTransform, targetPosition);
            canAttack = false;
        }
    }

	public override void StopAttack()
	{
        canAttack = false;
	}
}
