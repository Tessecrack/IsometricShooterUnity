using UnityEngine;

public class RangeAttackEvent : RangeAttack
{
	public RangeAttackEvent(in AnimationEvents animationEvents, in Shooter shooter) :
		base(shooter)
	{
		IsEventAttack = true;

		animationEvents.OnStartAttack += HandlerStartAttackEvent;
		animationEvents.OnEndAttack += HandlerEndAttackEvent;

		animationEvents.OnShot += HandlerShot;
	}

	public void HandlerStartAttackEvent()
	{
		if (IsAttackInProcess)
		{
			return;
		}
		IsStartAttack = true;
		IsAttackInProcess = true;
	}

	public void HandlerEndAttackEvent()
	{
		IsStartAttack = false;
		IsAttackInProcess = false;
	}

	public void HandlerShot()
	{
		shooter.Shot(this.targetPosition);
	}

	public override void StartAttack()
	{
		
	}
	public override void EndAttack()
	{
		
	}
}
