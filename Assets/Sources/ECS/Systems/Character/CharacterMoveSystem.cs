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
			.Inc<TargetComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var staticData = sharedData.StaticData;

		var characters = world.GetPool<CharacterComponent>();
		var inputs = world.GetPool<CharacterEventsComponent>();
		var movables = world.GetPool<MovableComponent>();
		var dashes = world.GetPool<DashComponent>();

		foreach(int entity in filter)
		{
			ref var characterComponent = ref characters.Get(entity);
			ref var inputComponent = ref inputs.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var dashComponent = ref dashes.Get(entity);

			var velocity = (staticData.GlobalForwardVector * inputComponent.inputMovement.z
				+ staticData.GlobalRightVector * inputComponent.inputMovement.x).normalized;

			movableComponent.relativeVector = Vector3.Normalize(movableComponent.transform.InverseTransformDirection(velocity));
			dashComponent.isStartDash = inputComponent.isDash;
			movableComponent.velocity = velocity;
			var speedMove = dashComponent.isActiveDash ? dashComponent.dashSpeed : movableComponent.moveSpeed;
			characterComponent.characterController.Move(velocity * speedMove * Time.deltaTime);
		}
	}
}
