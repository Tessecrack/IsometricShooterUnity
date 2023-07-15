using UnityEngine;

public class EnemyMeleeAnimationsManager : AnimationsManager
{
	private bool isAnimationAttackInProgress;
	private bool needUpdateAnimationsState;
	private int[] idsAnimationsStrikes;
	public EnemyMeleeAnimationsManager(Animator animator, AnimationEvents animationEvents) : base(animator)
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
		idsAnimationsStrikes[5] = HashCharacterAnimations.MeleeSixthAttack;
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
		SetParamsBlendTree(updatedAnimationsState);
		if (isAnimationAttackInProgress)
		{
			return;
		}

		if (updatedAnimationsState.IsMeleeAttack)
		{
			AnimateMeleeAttack(idsAnimationsStrikes[this.animationCounterAttacks.CurrentNumberAnimation]);			
			return;
		}

		if (currentAnimationState.Equals(updatedAnimationsState) && !needUpdateAnimationsState)
		{
			return;
		}
		needUpdateAnimationsState = false;
		PlayAnimation(HashCharacterAnimations.LocomotionRun);
		currentAnimationState.UpdateValuesState(updatedAnimationsState);
	}

	private void SetParamsBlendTree(CharacterAnimationState updatedAnimationsState)
	{
		animator.SetFloat(HashParamsAnimations.Horizontal, updatedAnimationsState.HorizontalMoveValue);
		animator.SetFloat(HashParamsAnimations.Vertical, updatedAnimationsState.VerticalMoveValue);
	}
}
