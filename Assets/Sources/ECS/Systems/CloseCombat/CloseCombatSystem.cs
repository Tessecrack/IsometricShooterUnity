using Leopotam.EcsLite;
using UnityEngine;

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
			if (!enabler.isEnabled)
			{
				continue;
			}
			ref var attackComponent = ref attackComponents.Get(entity);
			ref var closeCombatComponent = ref closeCombats.Get(entity);
			ref var stateAttack = ref stateAttackComponents.Get(entity);

			if (attackComponent.isStartAttack && attackComponent.typeWeapon == TypeWeapon.MELEE)
			{
				closeCombatComponent.closeCombat.SetNeedStrike();
				stateAttack.isMeleeAttack = true;
			}

			if (closeCombatComponent.closeCombat.IsEndAttack)
			{
				stateAttack.isMeleeAttack = false;
			}
		}
	}
}
