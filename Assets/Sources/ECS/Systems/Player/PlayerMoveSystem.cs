using Leopotam.EcsLite;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<CharacterComponent>()
			.Inc<InputEventComponent>()
			.Inc<MovableComponent>()
			.Inc<CharacterStateComponent>()
			.Inc<DashComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var staticData = sharedData.StaticData;
		var runtimeData = sharedData.RuntimeData;

		var characters = world.GetPool<CharacterComponent>();
		var inputs = world.GetPool<InputEventComponent>();
		var movables = world.GetPool<MovableComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();
		var dashes = world.GetPool<DashComponent>();

		foreach(int entity in filter)
		{
			ref var characterComponent = ref characters.Get(entity);
			ref var inputComponent = ref inputs.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var characterStateComponent = ref characterStates.Get(entity);
			ref var dashComponent = ref dashes.Get(entity);

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
