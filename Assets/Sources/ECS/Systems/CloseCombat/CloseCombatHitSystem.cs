using Leopotam.EcsLite;

public class CloseCombatHitSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<HealthComponent>()
			.Inc<HitComponent>()
			.Inc<EnablerComponent>()
			.End();

		var healths = world.GetPool<HealthComponent>();
		var hits = world.GetPool<HitComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach (int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var healthComponent = ref healths.Get(entity);
			ref var hit = ref hits.Get(entity);

			if (hit.isHitMe && !hit.wasAppliedDamage)
			{
				healthComponent.damageable.HitDamage(hit.damageHit, healthComponent.currentHealth);
				hit.wasAppliedDamage = true;
				hit.isHitMe = false;
			}
		}
	}
}
