using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

			if (hit.isHitMe)
			{
				healthComponent.damageable.HitDamage(50, healthComponent.currentHealth);
			}
		}
	}
}
