using Leopotam.EcsLite;

public class InputEventSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<InputEventComponent>().End();
		var inputs = world.GetPool<InputEventComponent>();

		foreach(int entity in filter)
		{
			ref var inputComponent = ref inputs.Get(entity);

			inputComponent.inputMovement.x = inputComponent.userInput.GetValueHorizontal();
			inputComponent.inputMovement.z = inputComponent.userInput.GetValueVertical();

			inputComponent.isStartAttack = inputComponent.userInput.IsStartFire;
			inputComponent.isStopAttack = inputComponent.userInput.IsStopFire;

			inputComponent.isDash = inputComponent.userInput.IsDash;

			inputComponent.selectedNumberWeapon = inputComponent.userInput.SelectedWeapon;
		}
	}
}
