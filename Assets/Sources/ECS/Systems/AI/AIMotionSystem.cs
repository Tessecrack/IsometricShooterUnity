using Leopotam.EcsLite;

public class AIMotionSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filter = world.Filter<AIComponent>()
			.Inc<CharacterEventsComponent>()
			.Inc<TargetComponent>()
			.Inc<VelocityComponent>()
			.End();

		var aiComponents = world.GetPool<AIComponent>();
		var eventsComponents = world.GetPool<CharacterEventsComponent>();
		var targets = world.GetPool<TargetComponent>();
		var velocities = world.GetPool<VelocityComponent>();

		foreach(var entity in filter)
		{
			ref var aiComponent = ref aiComponents.Get(entity);
			ref var characterEvent = ref eventsComponents.Get(entity);
			ref var target = ref targets.Get(entity);
			ref var velocity = ref velocities.Get(entity);

			velocity.velocity = aiComponent.aiAgent.MoveToTarget(target.target);
		}
	}
}
