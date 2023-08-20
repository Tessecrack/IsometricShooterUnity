using UnityEngine;

public class RangeAttackEvent : RangeAttack
{
	public RangeAttackEvent(in AnimationEvents animationEvents, in Shooter shooter) :
		base(shooter)
	{
		IsEventAttack = true;

		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;

		animationEvents.OnShot += shooter.Shot;
	}
}
