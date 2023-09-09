using UnityEngine;

public abstract class BaseAttack
{
	public bool IsStartAttack { get; protected set; }
	public bool IsEndAttack { get; protected set; }
	public bool IsAttackInProcess { get; protected set; }
	public bool IsEventAttack { get; protected set; }
	public bool IsApplyDamage { get; protected set; }
	public bool NeedForwardMove { get; protected set; }
	public TypeAttack TypeAttack { get; protected set; }

	protected bool canAttack;
	protected float passedTime;
	protected float delayBetweenAttack = 0.5f;
	protected Vector3 targetPosition;

	public abstract void StartAttack();
	public abstract void EndAttack();

	public void SetTargetPosition(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
	}

	public void SetDelayBetweenAttack(float delayBetweenAttack)
	{
		this.delayBetweenAttack = delayBetweenAttack;
	}

	public void UpdateTime(float time)
	{
		if (passedTime >= delayBetweenAttack)
		{
			canAttack = true;
			passedTime = 0.0f;
		}
		if (canAttack == false)
		{
			passedTime += time;
		}
	}
}
