using Leopotam.EcsLite;

public class DamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<HealthComponent>().End();

		var healths = world.GetPool<HealthComponent>();

		foreach(int entity in filter)
		{
			ref var healthComponent = ref healths.Get(entity);

			if (healthComponent.damageable.IsTakedDamage)
			{
				healthComponent.currentHealth = healthComponent.damageable.ApplyDamage(healthComponent.currentHealth);
			}
		}
	}
}
