using UnityEngine;

public class RangeAttack : BaseAttack
{
	protected Shooter shooter;

	public RangeAttack(Shooter shooter)
	{
		this.shooter = shooter;
		TypeAttack = TypeAttack.RANGE;
	}

	public override void StartAttack()
	{
		IsStartAttack = true;
		IsAttackInProcess = true;
		shooter.Shot(this.targetPosition);
	}

	public override void EndAttack()
	{
		IsAttackInProcess = false;
		IsStartAttack = false;
	}
}
