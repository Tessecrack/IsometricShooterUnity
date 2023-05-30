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
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var playerPosition = sharedData.RuntimeData.PlayerPosition;

		var aiEnemyComponents = world.GetPool<AIEnemyComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var stateComponents = world.GetPool<StateAttackComponent>();

		foreach(var entity in filter)
		{
			ref var aiEnemyComponent = ref aiEnemyComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);
			ref var stateComponent = ref stateComponents.Get(entity);

			if (aiEnemyComponent.enemyAgent.IsDetectTarget(playerPosition))
			{
				targetComponent.target = playerPosition;
				stateComponent.state = CharacterState.Aiming;
			}
		}
	}
}
