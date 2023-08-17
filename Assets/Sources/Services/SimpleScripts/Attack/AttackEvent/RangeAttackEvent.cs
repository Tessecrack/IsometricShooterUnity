using UnityEngine;

public class RangeAttackEvent : RangeAttack
{
	public RangeAttackEvent(in AnimationEvents animationEvents, in Transform pointSpawn, in Transform owner) :
		base(pointSpawn, owner)
	{
		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;

		animationEvents.OnShot += Shot;
	}
}
