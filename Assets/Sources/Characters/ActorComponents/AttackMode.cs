using UnityEngine;

public class AttackMode
{
	public float TimeAttackModeRangedWeapon { get; private set; }
	public bool IsActive { get; private set; }

	private float passedTime = 0.0f;

	public AttackMode()
	{
		TimeAttackModeRangedWeapon = 2.0f;
	}

	public void Enable()
	{
		IsActive = true;
		passedTime = 0.0f;
	}

	public void Disable()
	{
		if (passedTime >= TimeAttackModeRangedWeapon)
		{
			IsActive = false;
		}
	}

	public void ForceDisable()
	{
		IsActive = false;
		passedTime = 0.0f;
	}

	public void UpdateTimeAttackMode(float time)
	{
		if (IsActive == false)
		{
			return;
		}

		if (passedTime >= TimeAttackModeRangedWeapon)
		{
			IsActive = false;
			return;
		}

		if (passedTime <= TimeAttackModeRangedWeapon)
		{
			passedTime += time;
		}
	}

	public void StartAttack(Weapon weapon, Transform ownerTransform, Vector3 targetPoint)
	{
		Enable();
		weapon.StartAttack(ownerTransform, targetPoint);
	}

	public void StopAttack(Weapon weapon)
	{
		Disable();
		weapon.StopAttack();
	}
}
