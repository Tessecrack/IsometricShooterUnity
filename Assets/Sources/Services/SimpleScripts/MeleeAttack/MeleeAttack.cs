public class MeleeAttack
{
	private readonly int totalNumberStrikes = 4;
	private readonly int speedMoveForwardInStrike = 30;

	public int CurrentNumberStrike { get; private set; }
	public bool IsApplyDamage { get; private set; }
	public bool NeedForwardMove { get; private set; }
	public int TotalNumberStrikes => totalNumberStrikes;
	public int SpeedMoveForwardInStrike => speedMoveForwardInStrike;

	public bool AttackInProcess { get; private set; }

	public MeleeAttack(AnimationEvents animationEvents) 
	{
		this.totalNumberStrikes = animationEvents.CounterAnimations.TotalNumberAttacks;

		animationEvents.OnStartAttack += HandlerStartMeleeAttack;
		animationEvents.OnEndAttack += HandlerEndMeleeAttack;

		animationEvents.OnStartApplyDamage += HandlerStartApplyMeleeDamage;
		animationEvents.OnEndApplyDamage += HandlerEndApplyMeleeDamage;

		animationEvents.OnStartForwardMove += HandlerStartForwardMove;
		animationEvents.OnEndForwardMove += HandlerEndForwardMove;
	}
	public void StartAttack()
	{
		AttackInProcess = true;
	}

	public void HandlerStartMeleeAttack()
	{
		AttackInProcess = true;
	}

	public void HandlerEndMeleeAttack()
	{
		AttackInProcess = false;
	}

	public void HandlerStartApplyMeleeDamage()
	{
		IsApplyDamage = true;
	}

	public void HandlerEndApplyMeleeDamage()
	{
		IsApplyDamage = false;
	}	

	public void HandlerStartForwardMove()
	{
		NeedForwardMove = true;
	}

	public void HandlerEndForwardMove()
	{
		NeedForwardMove = false;
	}
}
