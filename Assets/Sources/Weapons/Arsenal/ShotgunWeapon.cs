using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class ShotgunWeapon : Weapon
{
    private ActorController owner;
    private Vector3 targetPosition;
    protected override void InitWeapon()
    {
        nameWeapon = "Shotgun";
        speedAttack = 80.0f;
        DelayBetweenAttack = 1.0f;
        CurrentTypeWeapon = TypeWeapon.HEAVY;
        base.InitWeapon();
    }

    public override void StartAttack(ActorController owner, Vector3 targetPosition)
    {
        if (canAttack && passedAttackTime >= DelayBetweenAttack)
        {
            this.owner = owner;
            this.targetPosition = targetPosition;
            StartCoroutine(GenerateSpreadBullets());
            passedAttackTime = 0;
            canAttack = false;
        }
    }
    IEnumerator GenerateSpreadBullets()
    {
		var shotgunShell = 5;
		var spreadShell = Vector3.Distance(targetPosition, owner.transform.position);
		base.StartAttack(owner, targetPosition);
		for (int i = 3; i <= shotgunShell; ++i)
		{
			base.StartAttack(owner, targetPosition + owner.transform.right * spreadShell / i);
			base.StartAttack(owner, targetPosition + owner.transform.right * -spreadShell / i);
			yield return new WaitForFixedUpdate();
		}
        yield break;
	}
}
