using Leopotam.EcsLite;
using UnityEngine;
public class CharacterRotationSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<RotatableComponent>()
			.Inc<CharacterComponent>()
			.Inc<CharacterStateComponent>()
			.Inc<TargetComponent>()
			.Inc<AimStateComponent>()
			.Inc<EnablerComponent>()
			.End();

		var rotatableComponents = world.GetPool<RotatableComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var aimStates = world.GetPool<AimStateComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var aimState = ref aimStates.Get(entity);

			if (aimState.aimState == AimState.AIM)
			{
				ref var characterComponent = ref characterComponents.Get(entity);
				ref var rotatableComponent = ref rotatableComponents.Get(entity);
				ref var targetComponent = ref targetComponents.Get(entity);
				var direction = targetComponent.target - characterComponent.characterTransform.position;
				direction.y = 0;
				characterComponent.characterTransform.forward = Vector3.Slerp(characterComponent.characterTransform.forward,
					direction, rotatableComponent.coefSmooth);
			}
		}
	}
}
