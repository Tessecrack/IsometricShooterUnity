using Leopotam.EcsLite;

public class PlayerDetectTargetSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<PlayerComponent>()
			.Inc<TargetComponent>()
			.End();
		var sharedData = systems.GetShared<SharedData>();
		var cursorPosition = sharedData.RuntimeData.CursorPosition;
		var targets = world.GetPool<TargetComponent>();
		foreach(var entity in filter)
		{
			ref var targetComponent = ref targets.Get(entity);
			targetComponent.target = cursorPosition;
		}
	}
}
