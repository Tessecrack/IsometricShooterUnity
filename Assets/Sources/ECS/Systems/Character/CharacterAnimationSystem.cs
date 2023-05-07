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

			animatorComponent.animationsManager.SetTypeWeapon(weaponComponent.typeWeapon);
			animatorComponent.animationsManager.SetRun(movableComponent.velocity.z != 0 || movableComponent.velocity.x != 0);
			animatorComponent.animationsManager.SetVerticalMotion(movableComponent.relativeVector.z);
			animatorComponent.animationsManager.SetHorizontalMotion(movableComponent.relativeVector.x);
			animatorComponent.animationsManager.SetAttackMode(currentState);
		}
	}
}
