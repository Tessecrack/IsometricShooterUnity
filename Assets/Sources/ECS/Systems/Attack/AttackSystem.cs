using Leopotam.EcsLite;

public class AttackSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackComponent>().Inc<WeaponComponent>().End();

		var attacks = world.GetPool<AttackComponent>();
		var weapons = world.GetPool<WeaponComponent>();

		foreach (int entity in filter)
		{
			ref var attackComponent = ref attacks.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);

			if (attackComponent.isStartAttack)
			{
				weaponComponent.weapon.StartAttack(attackComponent.attackerTransform, attackComponent.targetPoint);
			}
			if (attackComponent.isStopAttack)
			{
				weaponComponent.weapon.StopAttack();
			}	
		}
	}
}
