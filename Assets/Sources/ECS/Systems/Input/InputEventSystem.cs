using Leopotam.Ecs;
using UnityEngine;

public class InputEventSystem : IEcsRunSystem
{
	private EcsFilter<InputEventComponent> filter;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var inputComponent = ref filter.Get1(i);
			inputComponent.inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			inputComponent.isAttack = Input.GetMouseButton(0);
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
