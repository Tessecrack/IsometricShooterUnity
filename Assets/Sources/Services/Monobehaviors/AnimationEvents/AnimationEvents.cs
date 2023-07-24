using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
	[SerializeField] private int countAnimationAttacks = 4;

	public event Action OnStartAttack;
	public event Action OnEndAttack;

	public event Action OnStartApplyDamage;
	public event Action OnEndApplyDamage;

	public event Action OnStartForwardMove;
	public event Action OnEndForwardMove;

	public event Action OnShot;

	private float passedTimeLastAttack = 0;
	private readonly float timeReset = 0.4f;

	private bool isStartAnimationAttack;
	private bool isEndAnimationAttack;

	private AnimationCounterAttacks animationCounterAttacks;

	public void Init()
	{
		animationCounterAttacks = new AnimationCounterAttacks(countAnimationAttacks);
	}

	private void Update()
	{
		if (isEndAnimationAttack)
		{
			passedTimeLastAttack += Time.deltaTime;
		}
		if (isStartAnimationAttack)
		{
			passedTimeLastAttack = 0;
		}
		if (passedTimeLastAttack >= timeReset)
		{
			passedTimeLastAttack = 0;
			animationCounterAttacks.ResetCounter();
		}
	}

	public void HandlerStartAttack()
	{
		isStartAnimationAttack = true;
		isEndAnimationAttack = false;

		OnStartAttack?.Invoke();
		animationCounterAttacks.Increase();
	}

	public void HandlerEndAttack()
	{
		isStartAnimationAttack = false;
		isEndAnimationAttack = true;

		OnEndAttack?.Invoke();
	}

	public void HandlerShot()
	{
		OnShot?.Invoke();
	}

	public void HandlerStartApplyDamage()
	{
		OnStartApplyDamage?.Invoke();
	}

	public void HandlerEndApplyDamage()
	{
		OnEndApplyDamage?.Invoke();
	}

	public void HandlerStartForwardMove()
	{
		OnStartForwardMove?.Invoke();
	}

	public void HandlerEndForwardMove()
	{
		OnEndForwardMove?.Invoke();
	}

	public AnimationCounterAttacks CounterAnimations => animationCounterAttacks;
}
