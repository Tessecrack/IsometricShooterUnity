using UnityEngine;

public class RangeAttack : BaseAttack
{
	protected Vector3 targetPosition;

	protected Shooter shooter;

	protected float passedTime;

	protected bool canAttack;

	protected float delayBetweenAttack = 0.05f;

	public RangeAttack(Shooter shooter)
	{
		this.shooter = shooter;
	}

	public void SetTarget(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
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
