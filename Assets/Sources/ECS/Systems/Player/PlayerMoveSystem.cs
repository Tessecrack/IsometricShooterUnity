using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
	private EcsFilter<CharacterComponent, InputMovementComponent, MovableComponent> filter;
	private StaticData staticData;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var characterComponent = ref filter.Get1(i);
			ref var inputComponent = ref filter.Get2(i);
			ref var movableComponent = ref filter.Get3(i);

			var velocity = (staticData.GlobalForwardVector * inputComponent.inputMovement.z
				+ staticData.GlobalRightVector * inputComponent.inputMovement.x).normalized;

			movableComponent.relativeVector = movableComponent.transform.InverseTransformDirection(velocity);
			movableComponent.velocity = velocity;

			if (movableComponent.velocity.magnitude > 0)
			{
				movableComponent.transform.forward = velocity;
			}

			characterComponent.characterController.Move(movableComponent.velocity * movableComponent.speedMove * Time.fixedDeltaTime);
		}
	}
}
