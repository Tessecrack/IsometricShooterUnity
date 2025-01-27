using Leopotam.EcsLite;

public class EnemyDetectTargetSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filterEnemy = world.Filter<AIComponent>()
			.Inc<TargetComponent>()
			.Inc<AimStateComponent>()
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
		var aimStates = world.GetPool<AimStateComponent>();
		var inputEvents = world.GetPool<CharacterEventsComponent>(); 
		var enableComponents = world.GetPool<EnablerComponent>();
		
		foreach (var entityPlayer in filterPlayer)
		{
			ref var playerEnabler = ref playerEnablers.Get(entityPlayer);
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
				ref var aimState = ref aimStates.Get(entity);
				ref var eventComponent = ref inputEvents.Get(entity);

				targetComponent.target = playerPosition;

				if (playerEnabler.isEnabled == false)
				{
					aimState.aimState = AimState.NO_AIM;
					aiEnemyComponent.aiAgent.Disable();
				}

				if (aiEnemyComponent.aiAgent.IsDetectTarget(playerPosition))
				{
					aimState.aimState = AimState.AIM;
				}
				else
				{
					aimState.aimState = AimState.NO_AIM;
				}
				bool canMeleeAttack = aiEnemyComponent.aiAgent.CanMeleeAttack(playerPosition);
				bool canRangeAttack = aiEnemyComponent.aiAgent.CanRangeAttack(playerPosition);

				eventComponent.isStartAttack = canMeleeAttack || canRangeAttack;
				eventComponent.isStopAttack = !eventComponent.isStartAttack;
			}
		}
	}
}
