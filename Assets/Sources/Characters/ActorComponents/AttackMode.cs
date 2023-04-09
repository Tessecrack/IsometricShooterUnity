using UnityEngine;

public class AttackMode
{
	public float TimeAttackModeRangedWeapon { get; private set; }
	public bool IsNeedAttack { get; private set; }
	public bool IsInAttackMode { get; private set; }

	private float passedTime = 0.0f;

	public AttackMode()
	{
		TimeAttackModeRangedWeapon = 2.0f;
	}

	public void Enable()
	{
		IsInAttackMode = true;
		IsNeedAttack = true;
		passedTime = 0.0f;
	}

	public void Disable()
	{
		IsNeedAttack = false;
	}

	public void UpdateTimeAttackMode(float time)
	{
		if (IsInAttackMode == false)
		{
			return;
		}

		if (passedTime >= TimeAttackModeRangedWeapon)
		{
			IsInAttackMode = false;
			return;
		}

		if (passedTime <= TimeAttackModeRangedWeapon)
		{
			passedTime += time;
		}
	}
}
