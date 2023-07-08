using Leopotam.EcsLite;

public class DamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<HealthComponent>()
			.Inc<CharacterComponent>()
			.Inc<EnablerComponent>()
			.End();

		var healths = world.GetPool<HealthComponent>();
		var characters = world.GetPool<CharacterComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var healthComponent = ref healths.Get(entity);
			ref var characterComponent = ref characters.Get(entity);

			if (healthComponent.damageable.IsTakedDamage)
			{
				healthComponent.currentHealth = healthComponent.damageable.ApplyDamage(healthComponent.currentHealth);
			}
			
			if (healthComponent.damageable.IsDeath)
			{
				healthComponent.damageable.DisableObject();
				world.DelEntity(entity);
			}
		}
	}
}
