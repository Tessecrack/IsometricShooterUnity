using Leopotam.Ecs;
using UnityEngine;

public class InputMovementSystem : IEcsRunSystem
{
	private EcsFilter<InputMovementComponent> filter;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var inputComponent = ref filter.Get1(i);
			inputComponent.inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
		}
	}
}
