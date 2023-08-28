using UnityEngine;

public abstract class AnimationsManager
{
	protected Animator animator;
	protected CharacterAnimationState currentAnimationState;
	protected AnimationCounterAttacks animationCounterAttacks;

	protected bool isActiveLayer;

	protected int previousIdAnimation;

	public AnimationsManager(in Animator animator)
	{
		this.animator = animator;
		currentAnimationState = new CharacterAnimationState();
	}

	public abstract void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState);

	private void PlayAnimation(int hashId)
	{
		animator.CrossFade(hashId, 0.02f);
		previousIdAnimation = hashId;
	}

	protected void PlayAnimationWithCheck(int hashId)
	{
		if (previousIdAnimation == hashId)
		{
			return;
		}
		PlayAnimation(hashId);
		previousIdAnimation = hashId;
	}

	protected void SetLayer(int idLayer)
	{
		if (isActiveLayer)
		{
			return;
		}
		isActiveLayer = true;
		animator.SetLayerWeight(idLayer, 1.0f);
	}

	protected void ResetLayer(int idLayer)
	{
		if (!isActiveLayer)
		{
			return;
		}
		isActiveLayer = false;
		animator.SetLayerWeight(idLayer, 0.0f);
	}

	protected void AnimateAttack(int idAnimation)
	{
		PlayAnimation(idAnimation);
	}
}
