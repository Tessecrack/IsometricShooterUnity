using UnityEngine;

public class HashCharacterAnimations
{
	public static readonly int Idle = Animator.StringToHash("Idle");

	public static readonly int Run = Animator.StringToHash("Run");

	public static readonly int HeavyAimingIdle = Animator.StringToHash("HeavyAimingIdle");

	public static readonly int GunAimingIdle = Animator.StringToHash("GunAimingIdle");

	public static readonly int HeavyAimingRunBlendTree = Animator.StringToHash("HeavyAimingRun");

	public static readonly int GunAimingRunBlendTree = Animator.StringToHash("GunAimingRun");

	public static readonly int SwordSimpleFirstAttack = Animator.StringToHash("SwordSimpleFirstAttack");

	public static readonly int SwordSimpleSecondAttack = Animator.StringToHash("SwordSimpleSecondAttack");

	public static readonly int SwordStrongFirstAttack = Animator.StringToHash("SwordStrongFirstAttack");

	public static readonly int SwordStrongSecondAttack = Animator.StringToHash("SwordStrongSecondAttack");

	public static readonly int[] IdsSwordsAttack = new int[]
	{
		SwordSimpleFirstAttack, SwordSimpleSecondAttack, SwordStrongFirstAttack, SwordStrongSecondAttack,
	};
}
