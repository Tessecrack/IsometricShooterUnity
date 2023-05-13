using UnityEngine;

public class CharacterAnimationsManager : MonoBehaviour
{
    private Animator animator;
    private HashCharacterAnimations hashAnimations;
    private CharacterAnimationState currentAnimationState;
    private CharacterAnimationState updatedAnimationsState;

    void Start()
    {
        animator = GetComponent<Animator>();
        hashAnimations = new HashCharacterAnimations();
        currentAnimationState = new CharacterAnimationState();
    }

    public void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState)
    {        
        if (currentAnimationState.Equals(updatedAnimationsState)) 
        {
            return;
        }

        if (updatedAnimationsState.IsAttackState == true)
        {
            if (currentAnimationState.IsAttackState != updatedAnimationsState.IsAttackState)
            {
                PlayAnimation(hashAnimations.HeavyAimingIdle);
            }
		}
        else if (updatedAnimationsState.IsAttackState == false)
        {
			if (currentAnimationState.IsMoving != updatedAnimationsState.IsMoving)
            {
                if (updatedAnimationsState.IsMoving == true)
                {
                    PlayAnimation(hashAnimations.Run);
                }
                else
                {
                    PlayAnimation(hashAnimations.Idle);
                }
            }
        }
        currentAnimationState.UpdateValuesState(updatedAnimationsState);
	}

    private void UpdateAnimationsAttackState()
    {

    }

    private void UpdateAnimationsNoAttackState()
    {

    }
    private void PlayAnimation(int hashId)
    {
		animator.CrossFade(hashId, 0.05f);
	}
}
