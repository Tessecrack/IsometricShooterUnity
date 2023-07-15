using UnityEngine;

public abstract class AnimationsManager
{
	protected Animator animator;
	protected CharacterAnimationState currentAnimationState;
	protected AnimationCounterAttacks animationCounterAttacks;

	public AnimationsManager(Animator animator)
	{
		this.animator = animator;
		currentAnimationState = new CharacterAnimationState();
	}

	public abstract void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState);

	protected void PlayAnimation(int hashId)
	{
		animator.CrossFade(hashId, 0.02f);
	}

	protected void SetLayer(int idLayer)
	{
		animator.SetLayerWeight(idLayer, 1.0f);
	}

	protected void ResetLayer(int idLayer)
	{
		animator.SetLayerWeight(idLayer, 0.0f);
	}

	protected void AnimateMeleeAttack(int idAnimation)
	{
		PlayAnimation(idAnimation);
	}
}
