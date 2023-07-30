public abstract class AttackEvent
{
	protected int totalNumberStrikes = 4;
	protected readonly int speedMoveForwardInStrike = 30;

	public bool IsAttackInProcess { get; protected set; }

	public int CurrentNumberStrike { get; protected set; }
	public bool IsApplyDamage { get; protected set; }
	public bool NeedForwardMove { get; protected set; }
	public int TotalNumberStrikes => totalNumberStrikes;
	public int SpeedMoveForwardInStrike => speedMoveForwardInStrike;

	public TypeAttack TypeAttack { get; protected set; }
	public AttackEvent(AnimationEvents animationEvents)
	{
		animationEvents.OnStartAttack += HandlerStartAttack;
		animationEvents.OnEndAttack += HandlerEndAttack;
	}

	protected void HandlerStartAttack()
	{
		IsAttackInProcess = true;
	}

	protected void HandlerEndAttack()
	{
		IsAttackInProcess = false;
	}
}
