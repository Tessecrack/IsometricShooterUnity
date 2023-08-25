using Leopotam.EcsLite;
using UnityEngine;

public class CharacterChangeStateSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<VelocityComponent>()
			.Inc<CharacterStateComponent>()
			.Inc<EnablerComponent>()
			.End();

		var velocityComponents = world.GetPool<VelocityComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var velocity = ref velocityComponents.Get(entity);
			ref var characterState = ref characterStates.Get(entity);

			if (velocity.velocity == Vector3.zero)
			{
				characterState.characterState = CharacterState.IDLE;
			}
			else
			{
				characterState.characterState = CharacterState.RUN;
			}
		}
	}
}
