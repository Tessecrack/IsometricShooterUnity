using Leopotam.EcsLite;

public class DamageHitSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filter = world.Filter<HealthComponent>()
			.Inc<HitMeComponent>()
			.Inc<EnablerComponent>()
			.End();

		var healths = world.GetPool<HealthComponent>();
		var hitMeComponents = world.GetPool<HitMeComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach (var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var health = ref healths.Get(entity);
			ref var hitMe = ref hitMeComponents.Get(entity);

			if (hitMe.isHitMe && hitMe.wasAppliedDamageMe == false)
			{
				health.damageable.TakeDamage(hitMe.damageToMe);
				hitMe.wasAppliedDamageMe = true;
			}
		}
	}
}
