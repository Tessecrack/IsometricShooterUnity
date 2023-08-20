using Leopotam.EcsLite;
public class AttackWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackComponent>()
			.Inc<WeaponComponent>()
			.Inc<TargetComponent>()
			.Inc<EnablerComponent>()
			.End();

		var attacks = world.GetPool<AttackComponent>();
		var weapons = world.GetPool<WeaponComponent>();
		var targets = world.GetPool<TargetComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach (int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var attackComponent = ref attacks.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);
			ref var targetComponent = ref targets.Get(entity);

			if (attackComponent.isStartAttack)
			{
				//weaponComponent.weapon.StartAttack(attackComponent.attackerTransform, targetComponent.target);
			}
			if (attackComponent.isStopAttack)
			{
				//weaponComponent.weapon.StopAttack();
			}	
		}
	}
}
