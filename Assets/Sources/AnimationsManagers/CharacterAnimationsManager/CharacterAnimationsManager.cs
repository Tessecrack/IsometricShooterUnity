using UnityEngine;

public class CharacterAnimationsManager : MonoBehaviour
{
    private Animator animator;
    private CharacterAnimationState currentAnimationState;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        currentAnimationState = new CharacterAnimationState();
    }

    public void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState)
    {        
        if (currentAnimationState.Equals(updatedAnimationsState)) 
        {
            return;
        }

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
            ChangeAnimationAttakRangeWeapon(updatedAnimationsState.CurrentTypeWeapon);
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
    private void ChangeAnimationAttakRangeWeapon(TypeWeapon typeWeapon)
    {
        switch (typeWeapon)
        {
            case TypeWeapon.GUN:
                PlayAnimation(HashCharacterAnimations.GunAimingIdle);
                break;
            case TypeWeapon.HEAVY:
                PlayAnimation(HashCharacterAnimations.HeavyAimingIdle);
                break;
        }
    }

    private void SetLayer(int idLayer)
    {
		animator.SetLayerWeight(idLayer, 1.0f);
	}

    private void ResetLayer(int idLayer)
    {
        animator.SetLayerWeight(idLayer, 0.0f);
    }

    private void PlayAnimation(int hashId)
    {
		animator.CrossFade(hashId, 0.02f);
	}
}
