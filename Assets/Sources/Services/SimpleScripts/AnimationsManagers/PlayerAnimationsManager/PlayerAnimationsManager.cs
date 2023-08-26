using UnityEngine;

public class PlayerAnimationsManager : AnimationsManager
{
	private bool isAnimationAttackInProgress;
	private bool needUpdateAnimationsState;
	private int[] idsAnimationsStrikes;

	private bool isAimingGun;

	private bool isAimingHeavy;

	private bool isLocomotionIdle;

	private bool isLocomotionRun;

	public PlayerAnimationsManager(in Animator animator, in AnimationEvents animationEvents) : base(animator)
	{
		this.animationCounterAttacks = animationEvents.CounterAnimations;

		InitMeleeAttacksAnimations();

		animationEvents.OnStartAttack += HandlerStartAttackEvent;
		animationEvents.OnEndAttack += HandlerEndAttackEvent;
	}

	public void HandlerStartAttackEvent()
	{
		isAnimationAttackInProgress = true;
	}

	public void HandlerEndAttackEvent()
	{
		isAnimationAttackInProgress = false;
		needUpdateAnimationsState = true;
	}

	public void InitMeleeAttacksAnimations()
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

		if (updatedAnimationsState.IsAttack == true && updatedAnimationsState.TypeAttack == TypeAttack.MELEE)
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
		ChangeCharacterState(updatedAnimationsState);
		currentAnimationState.UpdateValuesState(updatedAnimationsState);
	}

	private void ChangeCharacterState(CharacterAnimationState updatedAnimationsState)
	{
		switch (updatedAnimationsState.CharacterState)
		{
			case CharacterState.IDLE:
				CharacterStateIdle(updatedAnimationsState);
				break;
			case CharacterState.WALK:
				CharacterStateWalk(updatedAnimationsState);
				break;
			case CharacterState.RUN:
				CharacterStateRun(updatedAnimationsState);
				break;
		}
	}

	private void CharacterStateIdle(CharacterAnimationState updatedAnimationsState)
	{
		switch (updatedAnimationsState.AimState)
		{
			case AimState.NO_AIM:
				CharacterNoAimingIdleState(updatedAnimationsState);
				break;
			case AimState.AIM:
				CharacterAimingIdleState(updatedAnimationsState);
				break;
		}
	}

	private void CharacterStateRun(CharacterAnimationState updatedAnimationsState)
	{
		switch (updatedAnimationsState.AimState)
		{
			case AimState.NO_AIM:
				CharacterNoAimingRunState(updatedAnimationsState);
				break;
			case AimState.AIM:
				CharacterAimingRunState(updatedAnimationsState);
				break;
		}
	}

	private void CharacterAimingIdleState(CharacterAnimationState updatedAnimationsState)
	{
		switch (updatedAnimationsState.CurrentTypeWeapon)
		{
			case TypeWeapon.MELEE:
				PlayAnimation(HashCharacterAnimations.LocomotionIdle);
				break;
			case TypeWeapon.GUN:
				SetBlendTreeGunAiming();
				break;
			case TypeWeapon.HEAVY:
				SetBlendTreeHeavyAiming();
				break;
		}
	}

	private void CharacterNoAimingIdleState(CharacterAnimationState updatedAnimationsState)
	{
		isAimingHeavy = false;
		isAimingGun = false;

		switch (updatedAnimationsState.CurrentTypeWeapon)
		{
			case TypeWeapon.MELEE:
				ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
				break;
			case TypeWeapon.GUN:
				ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
				break;
			case TypeWeapon.HEAVY:
				SetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
				break;
		}
		PlayAnimation(HashCharacterAnimations.LocomotionIdle);
	}

	private void CharacterAimingRunState(CharacterAnimationState updatedAnimationsState)
	{
		switch (updatedAnimationsState.CurrentTypeWeapon)
		{
			case TypeWeapon.MELEE:
				PlayAnimation(HashCharacterAnimations.LocomotionRun);
				break;
			case TypeWeapon.GUN:
				SetBlendTreeGunAiming();
				break;
			case TypeWeapon.HEAVY:
				SetBlendTreeHeavyAiming();
				break;
		}
	}

	private void CharacterNoAimingRunState(CharacterAnimationState updatedAnimationsState)
	{
		isAimingHeavy = false;
		isAimingGun = false;

		switch (updatedAnimationsState.CurrentTypeWeapon)
		{
			case TypeWeapon.MELEE:
				ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
				break;
			case TypeWeapon.GUN:
				ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
				break;
			case TypeWeapon.HEAVY:
				SetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
				break;
		}
		PlayAnimation(HashCharacterAnimations.LocomotionRun);
	}

	private void SetBlendTreeGunAiming()
	{
		if (isAimingGun)
		{
			return;
		}
		ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
		PlayAnimation(HashCharacterAnimations.GunAimingRunBlendTree);
		isAimingGun = true;
	}

	private void SetBlendTreeHeavyAiming()
	{
		if (isAimingHeavy)
		{
			return;
		}
		ResetLayer((int)CharacterAnimationLayers.ArmsHeavyNoAiming);
		PlayAnimation(HashCharacterAnimations.HeavyAimingRunBlendTree);
		isAimingHeavy = true;
	}

	private void SetParamsBlendTree(CharacterAnimationState updatedAnimationsState)
	{
		animator.SetFloat(HashParamsAnimations.Horizontal, updatedAnimationsState.HorizontalMoveValue);
		animator.SetFloat(HashParamsAnimations.Vertical, updatedAnimationsState.VerticalMoveValue);
	}

	private void CharacterStateWalk(CharacterAnimationState updatedAnimationsState)
	{

	}
}
