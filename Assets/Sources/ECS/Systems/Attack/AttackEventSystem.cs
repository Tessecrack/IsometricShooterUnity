using Leopotam.EcsLite;

public class AttackEventSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackEventComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var attackEvents = world.GetPool<AttackEventComponent>(); 
		var attackComponents = world.GetPool<AttackComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var stateAttackComponents = world.GetPool<StateAttackComponent>();

		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var attackComponent = ref attackComponents.Get(entity);
			ref var stateAttack = ref stateAttackComponents.Get(entity);
			ref var attackEvent = ref attackEvents.Get(entity);

			var isMeleeAttackType = attackEvent.attackEvent.TypeAttack == TypeAttack.Melee;
			var isRangeAttackType = attackEvent.attackEvent.TypeAttack == TypeAttack.Range;

			if (attackEvent.attackEvent.IsAttackInProcess)
			{
				continue;
			}
			else
			{
				if (isMeleeAttackType)
				{
					stateAttack.isMeleeAttack = false;
				}
				else if (isRangeAttackType)
				{
					stateAttack.isRangeAttack = false;
				}
			}

			if (stateAttack.state == CharacterState.Idle)
			{
				continue;
			}

			if (attackComponent.isStartAttack)
			{
				stateAttack.isMeleeAttack = isMeleeAttackType;
				stateAttack.isRangeAttack = isRangeAttackType;
			}
		}
	}
}
