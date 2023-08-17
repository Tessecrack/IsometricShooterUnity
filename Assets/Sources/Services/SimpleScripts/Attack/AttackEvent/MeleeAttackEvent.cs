public class MeleeAttackEvent : MeleeAttack
{
	public MeleeAttackEvent(in AnimationEvents animationEvents)
	{
		this.totalNumberStrikes = animationEvents.CounterAnimations.TotalNumberAttacks;

		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;

		animationEvents.OnStartApplyDamage += HandlerStartApplyMeleeDamage;
		animationEvents.OnEndApplyDamage += HandlerEndApplyMeleeDamage;

		animationEvents.OnStartForwardMove += HandlerStartForwardMove;
		animationEvents.OnEndForwardMove += HandlerEndForwardMove;
	}

	private void HandlerStartApplyMeleeDamage()
	{
		IsApplyDamage = true;
	}

	private void HandlerEndApplyMeleeDamage()
	{
		IsApplyDamage = false;
	}	

	private void HandlerStartForwardMove()
	{
		NeedForwardMove = true;
	}

	private void HandlerEndForwardMove()
	{
		NeedForwardMove = false;
	}
}
