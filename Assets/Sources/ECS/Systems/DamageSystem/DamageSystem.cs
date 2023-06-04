using Leopotam.EcsLite;

public class DamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<HealthComponent>()
			.Inc<CharacterComponent>()
			.End();

		var healths = world.GetPool<HealthComponent>();
		var characters = world.GetPool<CharacterComponent>();

		foreach(int entity in filter)
		{
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
