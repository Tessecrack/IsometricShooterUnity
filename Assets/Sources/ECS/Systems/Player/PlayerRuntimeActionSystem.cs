using Leopotam.EcsLite;
using UnityEngine;

public class PlayerRuntimeActionSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<PlayerComponent>()
			.Inc<MovableComponent>()
			.Inc<EnablerComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var movableComponent = world.GetPool<MovableComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var movable = ref movableComponent.Get(entity);
			sharedData.RuntimeData.PlayerPosition = movable.transform.position;
			sharedData.RuntimeData.OwnerCameraTransform = movable.transform.position;
		}
	}
}
