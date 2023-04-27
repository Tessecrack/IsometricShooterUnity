using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
	private EcsFilter<CharacterComponent, InputEventComponent, MovableComponent> filter;
	private StaticData staticData;
	private RuntimeData runtimeData;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var characterComponent = ref filter.Get1(i);
			ref var inputComponent = ref filter.Get2(i);
			ref var movableComponent = ref filter.Get3(i);

			var velocity = (staticData.GlobalForwardVector * inputComponent.inputMovement.z
				+ staticData.GlobalRightVector * inputComponent.inputMovement.x).normalized;

			var direction = inputComponent.isStartAttack ? runtimeData.CursorPosition - characterComponent.currentPosition.position : velocity;

			movableComponent.relativeVector = movableComponent.transform.InverseTransformDirection(velocity);
			movableComponent.velocity = velocity;
			
			if (movableComponent.velocity.magnitude > 0 || inputComponent.isStartAttack)
			{
				movableComponent.transform.forward = Vector3.Slerp(movableComponent.transform.forward, direction, 0.3f);
			}
			characterComponent.characterController.Move(movableComponent.velocity * movableComponent.speedMove * Time.deltaTime);
		}
	}
}
