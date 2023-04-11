using System.Collections;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    private Transform ownerTransform;
    private Vector3 targetPosition;
    protected override void InitWeapon()
    {
        nameWeapon = "Shotgun";
        speedAttack = 80.0f;
        DelayBetweenAttack = 1.0f;
        CurrentTypeWeapon = TypeWeapon.HEAVY;
        base.InitWeapon();
    }

    public override void StartAttack(Transform ownerTransform, Vector3 targetPosition)
    {
        if (canAttack && passedAttackTime >= DelayBetweenAttack)
        {
            this.ownerTransform = ownerTransform;
            this.targetPosition = targetPosition;
            StartCoroutine(GenerateSpreadBullets());
            passedAttackTime = 0;
            canAttack = false;
        }
    }
    IEnumerator GenerateSpreadBullets()
    {
		var shotgunShell = 5;
		var spreadShell = Vector3.Distance(targetPosition, ownerTransform.position);
		SpawnBullet(ownerTransform, targetPosition);
		for (int i = 3; i <= shotgunShell; ++i)
		{
			SpawnBullet(ownerTransform, targetPosition + ownerTransform.right * spreadShell / i);
			SpawnBullet(ownerTransform, targetPosition + ownerTransform.right * -spreadShell / i);
			yield return new WaitForFixedUpdate();
		}
        yield break;
	}

	public override void StopAttack()
	{
        canAttack = true;
	}
}
