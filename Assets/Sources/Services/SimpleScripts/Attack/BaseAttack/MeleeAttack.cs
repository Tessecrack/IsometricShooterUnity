public class MeleeAttack : BaseAttack
{
	protected int totalNumberStrikes = 4;
	protected readonly int speedMoveForwardInStrike = 30;

	public int TotalNumberStrikes => totalNumberStrikes;
	public int SpeedMoveForwardInStrike => speedMoveForwardInStrike;

	public MeleeAttack()
	{
		TypeAttack = TypeAttack.MELEE;
	}

	public override void StartAttack()
	{
		IsStartAttack = true;
		IsAttackInProcess = true;
		IsEndAttack = false;
	}

	public override void EndAttack()
	{
		IsStartAttack = false;
		IsAttackInProcess = false;
		IsEndAttack = true;
	}

	protected void HandlerStartApplyMeleeDamage()
	{
		IsApplyDamage = true;
	}

	protected void HandlerEndApplyMeleeDamage()
	{
		IsApplyDamage = false;
	}

	protected void HandlerStartForwardMove()
	{
		NeedForwardMove = true;
	}

	protected void HandlerEndForwardMove()
	{
		NeedForwardMove = false;
	}
}
