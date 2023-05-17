using Leopotam.EcsLite;

public class PlayerInputSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filter = world.Filter<InputEventComponent>().Inc<CharacterEventsComponent>().End();

		var inputs = world.GetPool<InputEventComponent>();
		var characterEvents = world.GetPool<CharacterEventsComponent>();

		foreach(var entity in filter)
		{
			ref var inputComponent = ref inputs.Get(entity);
			ref var characterEvent = ref characterEvents.Get(entity);

			characterEvent.inputMovement = inputComponent.inputMovement;
			characterEvent.isStartAttack = inputComponent.isStartAttack;
			characterEvent.isStopAttack = inputComponent.isStopAttack;
			characterEvent.isDash = inputComponent.isDash;
			characterEvent.selectedNumberWeapon = inputComponent.selectedNumberWeapon;
		}
	}
}
