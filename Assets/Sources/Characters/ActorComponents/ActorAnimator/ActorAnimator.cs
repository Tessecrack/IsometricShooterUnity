using UnityEngine;

public class ActorAnimator
{
	private ActorController actorController;
	private Animator animator;
	public ActorAnimator(ActorController controller, Animator animator)
	{
		this.actorController = controller;
		this.animator = animator;
	}

	public void Animate()
	{
		if (animator != null) // bad idea (need fix)
		{
			animator.SetFloat(AnimationParams.FLOAT_CURRENT_TYPE_WEAPON, (byte)actorController.CurrentTypeWeapon);
			animator.SetFloat(AnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, actorController.GetDirectionRightMovementValue());
			animator.SetFloat(AnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, actorController.GetDirectionForwardMovementValue());
			animator.SetBool(AnimationParams.BOOL_RUN_NAME_PARAM, actorController.GetRightMovementValue() != 0.0f
				|| actorController.GetForwardMovementValue() != 0.0f);
			animator.SetBool(AnimationParams.BOOL_ATTACK_MODE_NAME_PARAM, actorController.IsActiveAttackMode());
		}
	}
}
