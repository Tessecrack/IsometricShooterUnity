using Leopotam.EcsLite;
using UnityEngine;

public class CharacterMoveSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<CharacterComponent>()
			.Inc<CharacterEventsComponent>()
			.Inc<MovableComponent>()
			.Inc<CharacterStateAttackComponent>()
			.Inc<DashComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var staticData = sharedData.StaticData;
		var runtimeData = sharedData.RuntimeData;

		var characters = world.GetPool<CharacterComponent>();
		var inputs = world.GetPool<CharacterEventsComponent>();
		var movables = world.GetPool<MovableComponent>();
		var characterStates = world.GetPool<CharacterStateAttackComponent>();
		var dashes = world.GetPool<DashComponent>();

		foreach(int entity in filter)
		{
			ref var characterComponent = ref characters.Get(entity);
			ref var inputComponent = ref inputs.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var characterStateComponent = ref characterStates.Get(entity);
			ref var dashComponent = ref dashes.Get(entity);

			var isStateAttack = characterStateComponent.characterState == CharacterState.Aiming;

			var velocity = (staticData.GlobalForwardVector * inputComponent.inputMovement.z
				+ staticData.GlobalRightVector * inputComponent.inputMovement.x).normalized;

			var direction = isStateAttack ? runtimeData.CursorPosition - characterComponent.characterTransform.position : velocity;

			movableComponent.relativeVector = movableComponent.transform.InverseTransformDirection(velocity);
			movableComponent.velocity = velocity;
			dashComponent.isStartDash = inputComponent.isDash;

			if (movableComponent.velocity.magnitude > 0 || isStateAttack)
			{
				direction.y = 0;
				movableComponent.transform.forward = Vector3.Slerp(movableComponent.transform.forward, direction, 0.3f);
			}
			var speedMove = dashComponent.isActiveDash ? dashComponent.dashSpeed : movableComponent.moveSpeed;

			characterComponent.characterController.Move(movableComponent.velocity * speedMove * Time.deltaTime);
		}
	}
}
