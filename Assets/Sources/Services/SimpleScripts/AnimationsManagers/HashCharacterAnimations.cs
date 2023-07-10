using UnityEngine;

public class HashCharacterAnimations
{
	public static readonly int Idle = Animator.StringToHash("LocomotionIdle");

	public static readonly int Run = Animator.StringToHash("LocomotionRun");

	public static readonly int HeavyAimingIdle = Animator.StringToHash("HeavyAimingIdle");

	public static readonly int GunAimingIdle = Animator.StringToHash("GunAimingIdle");

	public static readonly int HeavyAimingRunBlendTree = Animator.StringToHash("HeavyAimingRun");

	public static readonly int GunAimingRunBlendTree = Animator.StringToHash("GunAimingRun");

	public static readonly int MeleeSimpleFirstAttack = Animator.StringToHash("MeleeSimpleFirstAttack");

	public static readonly int MeleeSimpleSecondAttack = Animator.StringToHash("MeleeSimpleSecondAttack");

	public static readonly int MeleeStrongFirstAttack = Animator.StringToHash("MeleeStrongFirstAttack");

	public static readonly int MeleeStrongSecondAttack = Animator.StringToHash("MeleeStrongSecondAttack");

	public static readonly int[] IdsMeleeAttacks = new int[]
	{
		MeleeSimpleFirstAttack, MeleeSimpleSecondAttack, MeleeStrongFirstAttack, MeleeStrongSecondAttack
	};
}
