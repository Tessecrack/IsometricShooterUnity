using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
	private EcsFilter<CharacterComponent, InputMovementComponent, MovableComponent> filter;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var characterComponent = ref filter.Get1(i);
			ref var inputComponent = ref filter.Get2(i);
			ref var movableComponent = ref filter.Get3(i);

			Vector3 direction = (movableComponent.transform.forward * inputComponent.inputMovement.z
				+ movableComponent.transform.right * inputComponent.inputMovement.x).normalized;

			characterComponent.characterController.Move(direction * movableComponent.speedMove * Time.fixedDeltaTime);
		}
	}
}
