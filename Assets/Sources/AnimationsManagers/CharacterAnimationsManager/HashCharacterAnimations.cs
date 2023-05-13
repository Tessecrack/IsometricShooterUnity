using UnityEngine;

public class HashCharacterAnimations
{
	public HashCharacterAnimations()
	{
		Idle = Animator.StringToHash("Idle");
		Run = Animator.StringToHash("Run");
		HeavyAimingIdle = Animator.StringToHash("HeavyAimingIdle");
	}
	public int Idle { get; private set; }
	public int Run { get; private set; }
	public int HeavyAimingIdle { get; private set; }
}
