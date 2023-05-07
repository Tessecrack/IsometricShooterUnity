using UnityEngine;

public class CharacterAnimationsManager : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetTypeWeapon(TypeWeapon typeWeapon)
    {
		animator.SetFloat(CharacterAnimationParams.FLOAT_TYPE_WEAPON_NAME_PARAM, (int)typeWeapon);
	}

    public void SetRun(bool isRun)
    {
        animator.SetBool(CharacterAnimationParams.BOOL_RUN_NAME_PARAM, isRun);
	}

    public void SetVerticalMotion(float valueVertical)
    {
		animator.SetFloat(CharacterAnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, valueVertical);
	}

    public void SetHorizontalMotion(float valueHorizontal) 
    {
		animator.SetFloat(CharacterAnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, valueHorizontal);
	}

    public void SetAttackMode(CharacterState currentState) 
    {
		animator.SetBool(CharacterAnimationParams.BOOL_IS_ATTACK_MODE_NAME_PARAM, currentState == CharacterState.ATTACK);
	}
}
