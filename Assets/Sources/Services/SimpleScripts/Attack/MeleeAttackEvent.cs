public class MeleeAttackEvent : AttackEvent
{
	public MeleeAttackEvent(in AnimationEvents animationEvents) : base(animationEvents)
	{
		this.totalNumberStrikes = animationEvents.CounterAnimations.TotalNumberAttacks;

		animationEvents.OnStartApplyDamage += HandlerStartApplyMeleeDamage;
		animationEvents.OnEndApplyDamage += HandlerEndApplyMeleeDamage;

		animationEvents.OnStartForwardMove += HandlerStartForwardMove;
		animationEvents.OnEndForwardMove += HandlerEndForwardMove;

		TypeAttack = TypeAttack.Melee;
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
