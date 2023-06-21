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

		}
	}
}
