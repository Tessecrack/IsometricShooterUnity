using Leopotam.EcsLite;
using UnityEngine;
public class CharacterRotationSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<RotatableComponent>()
			.Inc<CharacterComponent>()
			.Inc<TargetComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var rotatableComponents = world.GetPool<RotatableComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var characterStates = world.GetPool<StateAttackComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (!enabler.isEnabled)
			{
				continue;
			}

			ref var characterComponent = ref characterComponents.Get(entity);
			ref var rotatableComponent = ref rotatableComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);
			ref var characterState = ref characterStates.Get(entity);

			var isStateAttack = characterState.state == CharacterState.Aiming
				|| characterState.isMeleeAttack;

			if (isStateAttack)
			{
				var direction = targetComponent.target - characterComponent.characterTransform.position;
				direction.y = 0;
				characterComponent.characterTransform.forward = Vector3.Slerp(characterComponent.characterTransform.forward,
					direction, rotatableComponent.coefSmooth);
			}
		}
	}
}
