using UnityEngine;

public class EnemyMeleeAnimationsManager : AnimationsManager
{
	private CloseCombat closeCombat;

	private bool isAnimationAttackInProgress;
	private bool needUpdateAnimationsState;
	private int[] idsAnimationsStrikes;

	public EnemyMeleeAnimationsManager(Animator animator, CloseCombat closeCombat) : base(animator)
	{
		this.closeCombat = closeCombat;
		InitializeCloseCombatAnimations();

		this.closeCombat.EventStartAttack += StartAttack;
		this.closeCombat.EventEndAttack += EndAttack;
	}

	public void InitializeCloseCombatAnimations()
	{
		idsAnimationsStrikes = new int[closeCombat.TotalNumberStrikes];
		idsAnimationsStrikes[0] = HashCharacterAnimations.MeleeSimpleFirstAttack;
		idsAnimationsStrikes[1] = HashCharacterAnimations.MeleeSimpleSecondAttack;
		idsAnimationsStrikes[2] = HashCharacterAnimations.MeleeStrongFirstAttack;
		idsAnimationsStrikes[3] = HashCharacterAnimations.MeleeStrongSecondAttack;
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
		if (closeCombat.IsStartAttack)
		{
			AnimateMeleeStrike(idsAnimationsStrikes[closeCombat.GetNextRandomStrike()]);
			isAnimationAttackInProgress = true;
			return;
		}
		if (currentAnimationState.Equals(updatedAnimationsState))
		{
			return;
		}
		currentAnimationState.UpdateValuesState(updatedAnimationsState);
	}

	private void SetParamsBlendTree(CharacterAnimationState updatedAnimationsState)
	{
		animator.SetFloat(HashParamsAnimations.Horizontal, updatedAnimationsState.HorizontalMoveValue);
		animator.SetFloat(HashParamsAnimations.Vertical, updatedAnimationsState.VerticalMoveValue);
	}
}
