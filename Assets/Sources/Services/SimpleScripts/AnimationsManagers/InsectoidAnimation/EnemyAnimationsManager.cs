using UnityEngine;

public class EnemyAnimationsManager : AnimationsManager
{
	private bool isAnimationAttackInProgress;
	private bool needUpdateAnimationsState;

	public EnemyAnimationsManager(in Animator animator, in AnimationEvents animationEvents,
		TypeAttack typeAttack) : base(animator)
	{
		this.animationCounterAttacks = animationEvents.CounterAnimations;
		InitializeAttackAnimations(typeAttack);

		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;
	}

	public void StartAttack()
	{
		isAnimationAttackInProgress = true;
	}

	public void EndAttack()
	{
		isAnimationAttackInProgress = false;
		needUpdateAnimationsState = true;
		PlayAnimationWithCheck(HashCharacterAnimations.LocomotionRun);
	}

	public override void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState, float deltaTime)
	{
		currentDeltaTime = deltaTime;
		if (currentAnimationState.EqualsBlendTreeParams(updatedAnimationsState) == false)
		{
			SetParamsBlendTree(updatedAnimationsState);
		}
		
		if (isAnimationAttackInProgress)
		{
			return;
		}

		if (updatedAnimationsState.IsAttack)
		{
			if (updatedAnimationsState.TypeAttack == TypeAttack.MELEE)
			{
				AnimateAttack(idsAnimationsStrikes[this.animationCounterAttacks.RandomNumberAnimation]);
				return;
			}

			if (updatedAnimationsState.TypeAttack == TypeAttack.RANGE)
			{
				AnimateAttack(idsAnimationsStrikes[0]);
				return;
			}
		}

		if (currentAnimationState.Equals(updatedAnimationsState) && !needUpdateAnimationsState)
		{
			return;
		}
		needUpdateAnimationsState = false;
		PlayAnimationWithCheck(HashCharacterAnimations.LocomotionRun);
		currentAnimationState.UpdateValuesState(updatedAnimationsState);
	}

	private void SetParamsBlendTree(CharacterAnimationState updatedAnimationsState)
	{
		animator.SetFloat(HashParamsAnimations.Horizontal, updatedAnimationsState.HorizontalMoveValue);
		animator.SetFloat(HashParamsAnimations.Vertical, updatedAnimationsState.VerticalMoveValue);
	}
}

