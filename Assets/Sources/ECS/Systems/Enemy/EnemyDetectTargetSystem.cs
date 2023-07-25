using Leopotam.EcsLite;

public class EnemyDetectTargetSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filterEnemy = world.Filter<AIComponent>()
			.Inc<TargetComponent>()
			.Inc<StateAttackComponent>()
			.Inc<CharacterEventsComponent>()
			.Inc<EnablerComponent>()
			.End();

		var filterPlayer = world.Filter<PlayerComponent>()
			.Inc<CharacterComponent>()
			.Inc<EnablerComponent>()
			.End();

		var playerCharacterComponents = world.GetPool<CharacterComponent>();
		var playerEnablers = world.GetPool<EnablerComponent>();

		var aiEnemyComponents = world.GetPool<AIComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var stateComponents = world.GetPool<StateAttackComponent>();
		var inputEvents = world.GetPool<CharacterEventsComponent>(); 
		var enableComponents = world.GetPool<EnablerComponent>();
		
		foreach (var entityPlayer in filterPlayer)
		{
			ref var playerEnabler = ref playerEnablers.Get(entityPlayer);
			if (playerEnabler.isEnabled == false)
			{
				continue;
			}

			ref var playerCharacterComponent = ref playerCharacterComponents.Get(entityPlayer);
			var playerPosition = playerCharacterComponent.characterTransform.position;

			foreach (var entity in filterEnemy)
			{
				ref var enabler = ref enableComponents.Get(entity);
				if (enabler.isEnabled == false)
				{
					continue;
				}

				ref var aiEnemyComponent = ref aiEnemyComponents.Get(entity);
				ref var targetComponent = ref targetComponents.Get(entity);
				ref var stateComponent = ref stateComponents.Get(entity);
				ref var eventComponent = ref inputEvents.Get(entity);

				targetComponent.target = playerPosition;

				if (aiEnemyComponent.aiAgent.IsDetectTarget(playerPosition))
				{
					stateComponent.state = CharacterState.Aiming;
				}
				else
				{
					stateComponent.state = CharacterState.Idle;
				}
				bool canMeleeAttack = aiEnemyComponent.aiAgent.CanMeleeAttack(playerPosition);
				bool canRangeAttack = aiEnemyComponent.aiAgent.CanRangeAttack(playerPosition);

				eventComponent.isStartAttack = canMeleeAttack || canRangeAttack;
				stateComponent.isRangeAttack = canRangeAttack;

				eventComponent.isStopAttack = !eventComponent.isStartAttack;
			}
		}
	}
}
