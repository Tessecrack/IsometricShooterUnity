using Leopotam.EcsLite;

public class CharacterInputAttackSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<CharacterEventsComponent>()
			.Inc<InputAttackComponent>()
			.Inc<CharacterComponent>()
			.Inc<TargetComponent>()
			.Inc<EnablerComponent>()
			.Inc<BaseAttackComponent>()
			.End();

		var characterEvents = world.GetPool<CharacterEventsComponent>();
		var attacks = world.GetPool<InputAttackComponent>();
		var characters = world.GetPool<CharacterComponent>();
		var targets = world.GetPool<TargetComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var baseAttacks = world.GetPool<BaseAttackComponent>();

		foreach (int entity in filter)
		{
			ref var enablerComponent = ref enablers.Get(entity);
			if (enablerComponent.isEnabled == false)
			{
				continue;
			}
			ref var characterEvent = ref characterEvents.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterComponent = ref characters.Get(entity);
			ref var targetComponent = ref targets.Get(entity);
			ref var baseAttack = ref baseAttacks.Get(entity);

			attackComponent.isStartAttack = characterEvent.isStartAttack;
			
			attackComponent.isStopAttack = characterEvent.isStopAttack;

			attackComponent.attackerTransform = characterComponent.characterTransform;

			attackComponent.typeAttack = baseAttack.baseAttack.TypeAttack;
		}
	}
}
