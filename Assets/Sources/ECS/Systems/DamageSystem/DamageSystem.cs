using Leopotam.EcsLite;

public class DamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<HealthComponent>()
			.Inc<CharacterComponent>()
			.Inc<EnablerComponent>()
			.Inc<CharacterStateComponent>()
			.End();

		var healths = world.GetPool<HealthComponent>();
		var characters = world.GetPool<CharacterComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();
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
			ref var characterState = ref characterStates.Get(entity);

			if (healthComponent.damageable.IsTakedDamage)
			{
				healthComponent.currentHealth = healthComponent.damageable.ApplyDamage(healthComponent.currentHealth);
			}
			
			if (healthComponent.damageable.IsDeath)
			{
				characterState.characterState = CharacterState.DEATH;
				enabler.isEnabled = false;
				//healthComponent.damageable.DisableObject();
				//world.DelEntity(entity);
			}
		}
	}
}
