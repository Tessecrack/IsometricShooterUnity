using Leopotam.EcsLite;
using UnityEngine;

public class EnablerSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<EnablerComponent>().Exc<PlayerComponent>().End();

		var sharedData = systems.GetShared<SharedData>();
		var playerPosition = sharedData.RuntimeData.PlayerPosition;

		var enablerComponents = world.GetPool<EnablerComponent>();

		foreach(var entity in filter)
		{
			ref var enablerComponent = ref enablerComponents.Get(entity);
			var distance = Vector3.Distance(enablerComponent.instance.transform.position, playerPosition);

			if (enablerComponent.isEnabled)
			{
				continue;
			}

			if (distance <= 30)
			{
				enablerComponent.instance.SetActive(true);
				enablerComponent.isEnabled = true;
			}
		}
	}
}
