using System.Text;
using UnityEngine;

public abstract class AnimationsManager
{
	protected Animator animator;
	protected CharacterAnimationState currentAnimationState;
	protected AnimationCounterAttacks animationCounterAttacks;

	protected bool isActiveLayer;

	protected int previousIdAnimation;

	protected float currentDeltaTime;

	protected int[] idsAnimationsStrikes;

	protected AnimationEvents animationEvents;

	public AnimationsManager(in Animator animator, in AnimationEvents animationEvents)
	{
		this.animator = animator;
		this.currentAnimationState = new CharacterAnimationState();
		this.animationEvents = animationEvents;
	}

	public abstract void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState, float deltaTime);

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

	protected void InitializeAttackAnimations(TypeAttack typeAttack)
	{
		idsAnimationsStrikes = new int[this.animationCounterAttacks.TotalNumberAttacks];
		if (typeAttack == TypeAttack.MELEE)
		{
			InitAttacks("MeleeAttack_");
		}
		else
		{
			InitAttacks("RangeAttack_");
		}
	}

	private void PlayAnimation(int hashId)
	{
		animator.CrossFade(hashId, 3f * currentDeltaTime);
		previousIdAnimation = hashId;
	}

	private void InitAttacks(string typeAttack)
	{
		for (int i = 0; i < idsAnimationsStrikes.Length; ++i)
		{
			StringBuilder strBuilder = new StringBuilder();
			strBuilder.Append(typeAttack).Append(i);
			idsAnimationsStrikes[i] = Animator.StringToHash(strBuilder.ToString());
		}
	}
}
