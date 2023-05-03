using Leopotam.Ecs;
public class DamageSystem : IEcsRunSystem
{
	private EcsFilter<HealthComponent> filter;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var healthComponent = ref filter.Get1(i);

			if (healthComponent.damageable.IsTakedDamage)
			{
				healthComponent.currentHealth = healthComponent.damageable.ApplyDamage(healthComponent.currentHealth);
			}
		}
	}
}
