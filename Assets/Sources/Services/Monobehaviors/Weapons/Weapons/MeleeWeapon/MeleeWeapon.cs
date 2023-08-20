using UnityEngine;

public class MeleeWeapon : Weapon
{
	private MeleeAttackEvent meleeAttackEvent;

	public void SetMeleeAttackEvent(AnimationEvents animEvents)
	{
		meleeAttackEvent = new MeleeAttackEvent(animEvents);
		BaseAttack = meleeAttackEvent;
	}
}
