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

			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_TYPE_WEAPON_NAME_PARAM, (int)weaponComponent.typeWeapon);
			animatorComponent.animator.SetBool(AnimationParams.BOOL_RUN_NAME_PARAM, 
				movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0);
			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, movableComponent.relativeVector.z);
			animatorComponent.animator.SetFloat(AnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, movableComponent.relativeVector.x);
			animatorComponent.animator.SetBool(AnimationParams.BOOL_IS_ATTACK_MODE_NAME_PARAM, currentState == CharacterState.ATTACK);
		}
	}
}
