using UnityEngine;

public class RangeAttack : BaseAttack
{
	protected Shooter shooter;

	protected bool hasTarget = true;

	public RangeAttack(Shooter shooter)
	{
		this.shooter = shooter;
		TypeAttack = TypeAttack.RANGE;
	}

	public override void StartAttack()
	{
		IsStartAttack = true;
		IsAttackInProcess = true;
		if (hasTarget)
		{
			shooter.Shot(this.targetPosition);
		}
		else
		{
			shooter.Shot();
		}
	}

	public override void EndAttack()
	{
		IsAttackInProcess = false;
		IsStartAttack = false;
	}

	public void SetHasTarget(bool hasTarget)
	{
		this.hasTarget = hasTarget;
	}
}
