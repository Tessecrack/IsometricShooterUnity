using UnityEngine;

public class PlayerAnimationsManager : AnimationsManager
{
    private CloseCombat closeCombat;

    private bool isAnimationAttackInProgress;
    private bool needUpdateAnimationsState;
	private int[] idsAnimationsStrikes;

	public PlayerAnimationsManager(Animator animator, CloseCombat closeCombat) : base(animator)
    {
        this.closeCombat = closeCombat;
        InitializeCloseCombatAnimations();

        this.closeCombat.EventStartAttack += StartAttack;
		this.closeCombat.EventEndAttack += EndAttack;
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

	public void InitializeCloseCombatAnimations()
	{
        idsAnimationsStrikes = new int[closeCombat.TotalNumberStrikes];
		idsAnimationsStrikes[0] = HashCharacterAnimations.MeleeSimpleFirstAttack;
		idsAnimationsStrikes[1] = HashCharacterAnimations.MeleeSimpleSecondAttack;
		idsAnimationsStrikes[2] = HashCharacterAnimations.MeleeStrongFirstAttack;
		idsAnimationsStrikes[3] = HashCharacterAnimations.MeleeStrongSecondAttack;
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
			AnimateMeleeStrike(idsAnimationsStrikes[closeCombat.GetNextStrike()]);
            isAnimationAttackInProgress = true;
            return;
		}

		if (currentAnimationState.Equals(updatedAnimationsState) && !needUpdateAnimationsState) 
        {
            return;
        }
        needUpdateAnimationsState = false;
        bool isChangedAttackState = ChangeAttackState(updatedAnimationsState);
		
		if (updatedAnimationsState.IsAttackState == false)
        {
			ChangeLayerArmsHeavyNoAttack(updatedAnimationsState, isChangedAttackState);
			if (currentAnimationState.IsMoving != updatedAnimationsState.IsMoving || isChangedAttackState)
            {
                ChangeNoAttackMovingState(updatedAnimationsState);
            }
        }

        currentAnimationState.UpdateValuesState(updatedAnimationsState);
	}

    private bool ChangeAttackState(CharacterAnimationState updatedAnimationsState)
    {
        bool isChangedWeapon = updatedAnimationsState.CurrentTypeWeapon != currentAnimationState.CurrentTypeWeapon;

		if (currentAnimationState.IsAttackState != updatedAnimationsState.IsAttackState || 
            isChangedWeapon && updatedAnimationsState.IsAttackState == true)
		{
            ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAttack);
            ChangeAnimationAttackWeapon(updatedAnimationsState);
            return true;
		}
        return false;
	}

    private void ChangeNoAttackMovingState(CharacterAnimationState updatedAnimationsState)
    {
		if (updatedAnimationsState.IsMoving == true)
		{
			PlayAnimation(HashCharacterAnimations.Run);
		}
		else
		{
			PlayAnimation(HashCharacterAnimations.Idle);
		}
	}

    private void ChangeLayerArmsHeavyNoAttack(CharacterAnimationState updatedAnimationState, bool isChangedAttackState)
    {
        if (currentAnimationState.CurrentTypeWeapon == updatedAnimationState.CurrentTypeWeapon && isChangedAttackState == false)
        {
            return;
        }

        if (updatedAnimationState.CurrentTypeWeapon == TypeWeapon.HEAVY)
        {
            SetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAttack);
        }
        else
        {
            ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAttack);
		}
	}
    private void ChangeAnimationAttackWeapon(CharacterAnimationState animationState)
    {
        var typeWeapon = animationState.CurrentTypeWeapon;
        switch (typeWeapon)
        {
            case TypeWeapon.GUN:
                SetBlendTreeGunAttack();
                break;
            case TypeWeapon.HEAVY:
				SetBlendTreeHeavyAttack();
                break;
        }
    }

    private void SetBlendTreeGunAttack()
    {
        PlayAnimation(HashCharacterAnimations.GunAimingRunBlendTree);
    }

    private void SetBlendTreeHeavyAttack()
    {
		PlayAnimation(HashCharacterAnimations.HeavyAimingRunBlendTree);
	}

    private void SetParamsBlendTree(CharacterAnimationState updatedAnimationsState)
    {
        animator.SetFloat(HashParamsAnimations.Horizontal, updatedAnimationsState.HorizontalMoveValue);
        animator.SetFloat(HashParamsAnimations.Vertical, updatedAnimationsState.VerticalMoveValue);
    }
}
