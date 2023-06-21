using Leopotam.EcsLite;

public class CloseCombatSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<CloseCombatComponent>().Inc<EnablerComponent>().End();

		var closeCombats = world.GetPool<CloseCombatComponent>(); 
		var enablers = world.GetPool<EnablerComponent>();

		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (!enabler.isEnabled)
			{
				continue;
			}
			ref var closeCombatComponent = ref closeCombats.Get(entity);
		}
	}
}
