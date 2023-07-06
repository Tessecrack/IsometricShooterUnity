using Leopotam.EcsLite;
using UnityEngine;

public class PlayerRuntimeActionSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<PlayerComponent>()
			.Inc<MovableComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var movableComponent = world.GetPool<MovableComponent>();
		foreach(var entity in filter)
		{
			ref var movable = ref movableComponent.Get(entity);
			sharedData.RuntimeData.PlayerPosition = movable.transform.position;
			sharedData.RuntimeData.OwnerCameraTransform = movable.transform.position;
		}
	}
}
