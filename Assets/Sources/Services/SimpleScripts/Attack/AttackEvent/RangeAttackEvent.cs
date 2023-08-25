using UnityEngine;

public class RangeAttackEvent : RangeAttack
{
	public RangeAttackEvent(in AnimationEvents animationEvents, in Shooter shooter) :
		base(shooter)
	{
		IsEventAttack = true;

		animationEvents.OnStartAttack += HandlerStartAttackEvent;
		animationEvents.OnEndAttack += HandlerEndAttackEvent;

		animationEvents.OnShot += shooter.Shot;
	}

	public void HandlerStartAttackEvent()
	{
		if (IsAttackInProcess)
		{
			return;
		}
		base.StartAttack();
	}

	public void HandlerEndAttackEvent()
	{
		base.EndAttack();
	}

	public override void StartAttack()
	{
		
	}
	public override void EndAttack()
	{
		
	}
}
