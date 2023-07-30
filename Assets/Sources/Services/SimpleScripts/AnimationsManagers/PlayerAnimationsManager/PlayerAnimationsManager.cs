using UnityEngine;

public class PlayerAnimationsManager : AnimationsManager
{
    private bool isAnimationAttackInProgress;
    private bool needUpdateAnimationsState;
	private int[] idsAnimationsStrikes;

	public PlayerAnimationsManager(in Animator animator, in AnimationEvents animationEvents) : base(animator)
    {
        this.animationCounterAttacks = animationEvents.CounterAnimations;

        InitializeCloseCombatAnimations();

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
    }

	public void InitializeCloseCombatAnimations()
	{
        idsAnimationsStrikes = new int[this.animationCounterAttacks.TotalNumberAttacks];
		idsAnimationsStrikes[0] = HashCharacterAnimations.MeleeFirstAttack;
		idsAnimationsStrikes[1] = HashCharacterAnimations.MeleeSecondAttack;
		idsAnimationsStrikes[2] = HashCharacterAnimations.MeleeThirdAttack;
		idsAnimationsStrikes[3] = HashCharacterAnimations.MeleeFourthAttack;
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

		if (updatedAnimationsState.IsMeleeAttack == true)
		{
			AnimateAttack(idsAnimationsStrikes[this.animationCounterAttacks.CurrentNumberAnimation]);
            isAnimationAttackInProgress = true;
            return;
		}

		if (currentAnimationState.Equals(updatedAnimationsState) && !needUpdateAnimationsState) 
        {
            return;
        }
        needUpdateAnimationsState = false;
        bool isChangedAttackState = ChangeAttackState(updatedAnimationsState);
		
		if (updatedAnimationsState.IsAimingState == false)
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

		if (currentAnimationState.IsAimingState != updatedAnimationsState.IsAimingState || 
            isChangedWeapon && updatedAnimationsState.IsAimingState == true)
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
			PlayAnimation(HashCharacterAnimations.LocomotionRun);
		}
		else
		{
			PlayAnimation(HashCharacterAnimations.LocomotionIdle);
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
