using Leopotam.EcsLite;

public class PlayerInputSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filter = world.Filter<InputEventComponent>()
			.Inc<CharacterEventsComponent>()
			.Inc<VelocityComponent>()
			.End();

		var inputs = world.GetPool<InputEventComponent>();
		var characterEvents = world.GetPool<CharacterEventsComponent>();
		var velocityComponents = world.GetPool<VelocityComponent>();
		var sharedData = systems.GetShared<SharedData>();
		var staticData = sharedData.StaticData;
		foreach (var entity in filter)
		{
			ref var inputComponent = ref inputs.Get(entity);
			ref var characterEvent = ref characterEvents.Get(entity);
			ref var velocity = ref velocityComponents.Get(entity);

			velocity.velocity = (staticData.GlobalForwardVector * inputComponent.inputMovement.z
				+ staticData.GlobalRightVector * inputComponent.inputMovement.x).normalized;

			characterEvent.inputMovement = inputComponent.inputMovement;
			characterEvent.isStartAttack = inputComponent.isStartAttack;
			characterEvent.isStopAttack = inputComponent.isStopAttack;
			characterEvent.isDash = inputComponent.isDash;
			characterEvent.selectedNumberWeapon = inputComponent.selectedNumberWeapon;
		}
	}
}
