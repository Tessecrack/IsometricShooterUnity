using Leopotam.Ecs;

public class AttackSystem : IEcsRunSystem
{
	private EcsFilter<AttackComponent, WeaponComponent> filter;
	public void Run()
	{
		foreach (var i in filter)
		{
			ref var attackComponent = ref filter.Get1(i);
			ref var weaponComponent = ref filter.Get2(i);

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
