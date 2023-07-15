using Leopotam.EcsLite;

public class CloseCombatSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<CloseCombatComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var closeCombats = world.GetPool<CloseCombatComponent>(); 
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
			if (attackComponent.typeAttack == TypeAttack.Range)
			{
				continue;
			}
			ref var stateAttack = ref stateAttackComponents.Get(entity);
			if (stateAttack.state == CharacterState.Idle)
			{
				continue;
			}
			ref var closeCombatComponent = ref closeCombats.Get(entity);
			if (closeCombatComponent.closeCombat.AttackInProccess)
			{
				continue;
			}

			if (attackComponent.isStartAttack)
			{
				closeCombatComponent.closeCombat.StartAttack();
				stateAttack.isMeleeAttack = true;
			}

			if (closeCombatComponent.closeCombat.AttackInProccess == false)
			{
				stateAttack.isMeleeAttack = false;
			}
		}
	}
}
