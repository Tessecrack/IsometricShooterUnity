using Leopotam.EcsLite;

public class MeleeAttackSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<MeleeAttackComponent>()
			.Inc<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var meleeAttacks = world.GetPool<MeleeAttackComponent>(); 
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
			ref var meleeAttack = ref meleeAttacks.Get(entity);
			if (meleeAttack.meleeAttack.AttackInProcess)
			{
				continue;
			}

			if (attackComponent.isStartAttack)
			{
				meleeAttack.meleeAttack.StartAttack();
				stateAttack.isMeleeAttack = true;
			}

			if (meleeAttack.meleeAttack.AttackInProcess == false)
			{
				stateAttack.isMeleeAttack = false;
			}
		}
	}
}
