using Leopotam.EcsLite;

public class DamageHitSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filter = world.Filter<HealthComponent>()
			.Inc<HitMeComponent>()
			.End();

		var healths = world.GetPool<HealthComponent>();
		var hitMeComponents = world.GetPool<HitMeComponent>();

		foreach (var entity in filter)
		{
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
