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
        canAttack = true;
	}
}
