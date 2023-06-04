using Leopotam.EcsLite;
using UnityEngine;

public class EnemyDetectTargetSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AIEnemyComponent>()
			.Inc<TargetComponent>()
			.Inc<StateAttackComponent>()
			.Inc<CharacterEventsComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var playerPosition = sharedData.RuntimeData.PlayerPosition;

		var aiEnemyComponents = world.GetPool<AIEnemyComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var stateComponents = world.GetPool<StateAttackComponent>();
		var inputEvents = world.GetPool<CharacterEventsComponent>(); 

		foreach(var entity in filter)
		{
			ref var aiEnemyComponent = ref aiEnemyComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);
			ref var stateComponent = ref stateComponents.Get(entity);
			ref var eventComponent = ref inputEvents.Get(entity);

			if (aiEnemyComponent.enemyAgent.IsDetectTarget(playerPosition))
			{
				targetComponent.target = playerPosition;
				stateComponent.state = CharacterState.Aiming;
				eventComponent.isStartAttack = true;
			}
			else
			{
				stateComponent.state = CharacterState.Rest;
				eventComponent.isStartAttack = false;
			}
		}
	}
}
