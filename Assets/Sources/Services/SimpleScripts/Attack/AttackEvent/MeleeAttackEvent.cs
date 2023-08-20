public class MeleeAttackEvent : MeleeAttack
{
	public MeleeAttackEvent(in AnimationEvents animationEvents)
	{
		this.totalNumberStrikes = animationEvents.CounterAnimations.TotalNumberAttacks;

		IsEventAttack = true;

		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;

		animationEvents.OnStartApplyDamage += HandlerStartApplyMeleeDamage;
		animationEvents.OnEndApplyDamage += HandlerEndApplyMeleeDamage;

		animationEvents.OnStartForwardMove += HandlerStartForwardMove;
		animationEvents.OnEndForwardMove += HandlerEndForwardMove;
	}
}
