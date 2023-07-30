using UnityEngine;

public class EnemyAnimationsManager : AnimationsManager
{
	private bool isAnimationAttackInProgress;
	private bool needUpdateAnimationsState;
	private int[] idsAnimationsStrikes;

	private bool canPlayRangeAttack;

	public EnemyAnimationsManager(in Animator animator, in AnimationEvents animationEvents) : base(animator)
	{
		this.animationCounterAttacks = animationEvents.CounterAnimations;
		InitializeCloseCombatAnimations();

		animationEvents.OnStartAttack += StartAttack;
		animationEvents.OnEndAttack += EndAttack;
	}

	public void InitializeCloseCombatAnimations()
	{
		idsAnimationsStrikes = new int[this.animationCounterAttacks.TotalNumberAttacks];
		idsAnimationsStrikes[0] = HashCharacterAnimations.MeleeFirstAttack;
		idsAnimationsStrikes[1] = HashCharacterAnimations.MeleeSecondAttack;
		idsAnimationsStrikes[2] = HashCharacterAnimations.MeleeThirdAttack;
		idsAnimationsStrikes[3] = HashCharacterAnimations.MeleeFourthAttack;
		idsAnimationsStrikes[4] = HashCharacterAnimations.MeleeFifthAttack;
	}

	public void StartAttack()
	{
		isAnimationAttackInProgress = true;
	}

	public void EndAttack()
	{
		isAnimationAttackInProgress = false;
		needUpdateAnimationsState = true;
	}

	public override void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState)
	{
		if (currentAnimationState.EqualsBlendTreeParams(updatedAnimationsState) == false)
		{
			SetParamsBlendTree(updatedAnimationsState);
		}
		
		if (isAnimationAttackInProgress)
		{
			return;
		}

		if (updatedAnimationsState.IsMeleeAttack)
		{
			AnimateAttack(idsAnimationsStrikes[this.animationCounterAttacks.RandomNumberAnimation]);			
			return;
		}

		if (updatedAnimationsState.IsRangeAttack && canPlayRangeAttack)
		{
			canPlayRangeAttack = false;
			AnimateAttack(HashCharacterAnimations.RangeFirstAttack);
			return;
		}

		if (currentAnimationState.Equals(updatedAnimationsState) && !needUpdateAnimationsState)
		{
			return;
		}
		needUpdateAnimationsState = false;
		PlayAnimation(HashCharacterAnimations.LocomotionRun);
		canPlayRangeAttack = true;
		currentAnimationState.UpdateValuesState(updatedAnimationsState);
		
	}

	private void SetParamsBlendTree(CharacterAnimationState updatedAnimationsState)
	{
		animator.SetFloat(HashParamsAnimations.Horizontal, updatedAnimationsState.HorizontalMoveValue);
		animator.SetFloat(HashParamsAnimations.Vertical, updatedAnimationsState.VerticalMoveValue);
	}
}
