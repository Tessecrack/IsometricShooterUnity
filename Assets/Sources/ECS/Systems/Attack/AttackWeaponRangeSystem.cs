using Leopotam.EcsLite;

public class AttackWeaponRangeSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackComponent>()
			.Inc<WeaponComponent>()
			.Inc<TargetComponent>()
			.End();

		var attacks = world.GetPool<AttackComponent>();
		var weapons = world.GetPool<WeaponComponent>();
		var targets = world.GetPool<TargetComponent>();

		foreach (int entity in filter)
		{
			ref var attackComponent = ref attacks.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);
			ref var targetComponent = ref targets.Get(entity);
			if (weaponComponent.weapon.TypeWeapon == TypeWeapon.MELEE)
			{
				attackComponent.typeAttack = TypeAttack.Melee;
				continue;
			}
			else
			{
				attackComponent.typeAttack = TypeAttack.Range;
			}

			if (attackComponent.isStartAttack)
			{
				weaponComponent.weapon.StartAttack(attackComponent.attackerTransform, targetComponent.target);
			}
			if (attackComponent.isStopAttack)
			{
				weaponComponent.weapon.StopAttack();
			}	
		}
	}
}
