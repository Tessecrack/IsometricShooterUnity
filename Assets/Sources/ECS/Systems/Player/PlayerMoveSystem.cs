using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
	private EcsFilter<CharacterComponent, InputEventComponent, MovableComponent, CharacterStateComponent, DashComponent> filter;
	private StaticData staticData;
	private RuntimeData runtimeData;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var characterComponent = ref filter.Get1(i);
			ref var inputComponent = ref filter.Get2(i);
			ref var movableComponent = ref filter.Get3(i);
			ref var characterStateComponent = ref filter.Get4(i);
			ref var dashComponent = ref filter.Get5(i);

			var isStateAttack = characterStateComponent.characterState == CharacterState.ATTACK;

			var velocity = (staticData.GlobalForwardVector * inputComponent.inputMovement.z
				+ staticData.GlobalRightVector * inputComponent.inputMovement.x).normalized;

			var direction = isStateAttack ? runtimeData.CursorPosition - characterComponent.currentPosition.position : velocity;

			movableComponent.relativeVector = movableComponent.transform.InverseTransformDirection(velocity);
			movableComponent.velocity = velocity;
			dashComponent.isStartDash = inputComponent.isDash;

			if (movableComponent.velocity.magnitude > 0 || isStateAttack)
			{
				movableComponent.transform.forward = Vector3.Slerp(movableComponent.transform.forward, direction, 0.3f);
			}
			var speedMove = dashComponent.isActiveDash ? dashComponent.dashSpeed : movableComponent.moveSpeed;

			characterComponent.characterController.Move(movableComponent.velocity * speedMove * Time.deltaTime);
		}
	}
}
