using Leopotam.Ecs;
using UnityEngine;

public class CharacterAnimationSystem : IEcsRunSystem
{
	private EcsFilter<AnimatorComponent, MovableComponent, WeaponComponent, AttackComponent, CharacterStateComponent> filter;

	public void Run()
	{
		foreach(var i in filter)
		{
			ref var animatorComponent = ref filter.Get1(i);
			ref var movableComponent = ref filter.Get2(i);
			ref var weaponComponent = ref filter.Get3(i);
			ref var attackComponent = ref filter.Get4(i);
			ref var characterStateComponent = ref filter.Get5(i);

			var currentState = characterStateComponent.characterState;

			bool isHeavyWeapon = weaponComponent.typeWeapon == TypeWeapon.HEAVY;
			bool isGunWeaponAttack = weaponComponent.typeWeapon == TypeWeapon.GUN && currentState == CharacterState.ATTACK;

			animatorComponent.animator.SetLayerWeight((int)CharacterAnimationLayers.ArmsHeavyWeapon, isHeavyWeapon ? 1 : 0);
			animatorComponent.animator.SetLayerWeight((int)CharacterAnimationLayers.ArmsGunWeaponAttack, isGunWeaponAttack ? 1 : 0);

			animatorComponent.animator.SetFloat(CharacterAnimationParams.FLOAT_TYPE_WEAPON_NAME_PARAM, (int)weaponComponent.typeWeapon);
			animatorComponent.animator.SetBool(CharacterAnimationParams.BOOL_RUN_NAME_PARAM, 
				movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0);
			animatorComponent.animator.SetFloat(CharacterAnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, movableComponent.relativeVector.z);
			animatorComponent.animator.SetFloat(CharacterAnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, movableComponent.relativeVector.x);
			animatorComponent.animator.SetBool(CharacterAnimationParams.BOOL_IS_ATTACK_MODE_NAME_PARAM, currentState == CharacterState.ATTACK);
		}
	}
}
