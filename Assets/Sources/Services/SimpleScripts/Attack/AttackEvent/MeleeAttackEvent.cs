using System.Diagnostics;

public class MeleeAttackEvent : MeleeAttack
{
	public MeleeAttackEvent(in AnimationEvents animationEvents)
	{
		this.totalNumberStrikes = animationEvents.CounterAnimations.TotalNumberAttacks;

		IsEventAttack = true;

		animationEvents.OnStartAttack += HandlerStartAttackEvent;
		animationEvents.OnEndAttack += HandlerEndAttackEvent;

		animationEvents.OnStartApplyDamage += HandlerStartApplyMeleeDamage;
		animationEvents.OnEndApplyDamage += HandlerEndApplyMeleeDamage;

		animationEvents.OnStartForwardMove += HandlerStartForwardMove;
		animationEvents.OnEndForwardMove += HandlerEndForwardMove;
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
