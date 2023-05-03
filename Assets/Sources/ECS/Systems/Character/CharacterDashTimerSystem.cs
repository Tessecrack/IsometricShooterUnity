using Leopotam.Ecs;
using UnityEngine;

public class CharacterDashTimerSystem : IEcsRunSystem
{
	private EcsFilter<DashComponent> filter;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var dashComponent = ref filter.Get1(i);

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
