using UnityEngine;

public class HashCharacterAnimations
{
	public static readonly int LocomotionIdle = Animator.StringToHash("LocomotionIdle");

	public static readonly int LocomotionRun = Animator.StringToHash("LocomotionRun");

	public static readonly int HeavyAimingIdle = Animator.StringToHash("HeavyAimingIdle");

	public static readonly int GunAimingIdle = Animator.StringToHash("GunAimingIdle");

	public static readonly int HeavyAimingRunBlendTree = Animator.StringToHash("HeavyAimingRun");

	public static readonly int GunAimingRunBlendTree = Animator.StringToHash("GunAimingRun");

	public static readonly int MeleeFirstAttack = Animator.StringToHash("MeleeFirstAttack");

	public static readonly int MeleeSecondAttack = Animator.StringToHash("MeleeSecondAttack");

	public static readonly int MeleeThirdAttack = Animator.StringToHash("MeleeThirdAttack");

	public static readonly int MeleeFourthAttack = Animator.StringToHash("MeleeFourthAttack");

	public static readonly int MeleeFifthAttack = Animator.StringToHash("MeleeFifthAttack");

	public static readonly int RangeFirstAttack = Animator.StringToHash("RangeFirstAttack");

	public static readonly int[] IdsMeleeAttacks = new int[]
	{
		MeleeFirstAttack, MeleeSecondAttack, MeleeThirdAttack, MeleeFourthAttack
	};
}
