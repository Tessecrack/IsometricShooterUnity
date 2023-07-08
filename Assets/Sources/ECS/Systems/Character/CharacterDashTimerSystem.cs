using Leopotam.EcsLite;
using UnityEngine;

public class CharacterDashTimerSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<DashComponent>().End();

		var dashes = world.GetPool<DashComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var dashComponent = ref dashes.Get(entity);

			if (dashComponent.isStartDash)
			{
				dashComponent.isActiveDash = true;
				dashComponent.isStartDash = false;
				dashComponent.passedDashTime = 0;
			}

			if (dashComponent.isActiveDash)
			{
				dashComponent.passedDashTime += Time.deltaTime;
			}

			if (dashComponent.passedDashTime >= dashComponent.dashTime)
			{
				dashComponent.isActiveDash = false;
				dashComponent.passedDashTime = 0;
			}
		}
	}
}
