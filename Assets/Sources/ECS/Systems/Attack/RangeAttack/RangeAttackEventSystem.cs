using Leopotam.EcsLite;

public class RangeAttackEventSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<RangeAttackComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var attackEvents = world.GetPool<RangeAttackComponent>();
		var attackComponents = world.GetPool<AttackComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var stateAttackComponents = world.GetPool<StateAttackComponent>();

		foreach (var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var attackComponent = ref attackComponents.Get(entity);
			if (attackComponent.typeAttack == TypeAttack.Melee)
			{
				continue;
			}
			ref var stateAttack = ref stateAttackComponents.Get(entity);
			ref var attackEvent = ref attackEvents.Get(entity);

			if (attackEvent.rangeAttack.IsAttackInProcess)
			{
				continue;
			}
			else
			{
				stateAttack.isRangeAttack = false;
			}

			if (stateAttack.state == CharacterState.Idle)
			{
				continue;
			}

			if (attackComponent.isStartAttack)
			{
				stateAttack.isRangeAttack = true;
			}


		}
	}
}
