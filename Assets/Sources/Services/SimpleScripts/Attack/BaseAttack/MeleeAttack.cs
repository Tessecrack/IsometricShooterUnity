public class MeleeAttack : IAttack
{
	protected int totalNumberStrikes = 4;
	protected readonly int speedMoveForwardInStrike = 30;

	public int TotalNumberStrikes => totalNumberStrikes;
	public int SpeedMoveForwardInStrike => speedMoveForwardInStrike;

	public int CurrentNumberStrike { get; protected set; }
	public bool IsApplyDamage { get; protected set; }
	public bool NeedForwardMove { get; protected set; }
	public bool IsStartAttack { get; protected set; }
	public bool IsAttackInProcess { get; protected set; }

	public void StartAttack()
	{
		IsStartAttack = true;
		IsAttackInProcess = true;
	}

	public void EndAttack()
	{
		IsStartAttack = false;
		IsAttackInProcess = false;
	}
}
