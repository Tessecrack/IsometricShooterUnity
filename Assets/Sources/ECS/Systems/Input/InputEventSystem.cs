using Leopotam.EcsLite;
using UnityEngine;

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
			inputComponent.inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			inputComponent.isStartAttack = Input.GetMouseButton(0);
			inputComponent.isStopAttack = Input.GetMouseButtonUp(0);
			inputComponent.isDash = Input.GetKeyDown(KeyCode.LeftShift);
			inputComponent.selectedNumberWeapon = GetNumberSelectedWeapon(inputComponent.selectedNumberWeapon);
		}
	}

	private int GetNumberSelectedWeapon(int currentNumber)
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			return 0;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			return 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			return 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			return 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			return 4;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			return 5;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			return 6;
		}

		return currentNumber;
	}
}
