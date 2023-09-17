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
			.Inc<DashComponent>()
			.Inc<TargetComponent>()
			.Inc<EnablerComponent>()
			.Inc<VelocityComponent>()
			.Inc<AimStateComponent>()
			.End();

		var characters = world.GetPool<CharacterComponent>();
		var inputs = world.GetPool<CharacterEventsComponent>();
		var movables = world.GetPool<MovableComponent>();
		var dashes = world.GetPool<DashComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var velocityComponents = world.GetPool<VelocityComponent>();
		var aimStates = world.GetPool<AimStateComponent>();

		foreach(int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			
			ref var characterComponent = ref characters.Get(entity);
			ref var inputComponent = ref inputs.Get(entity);
			ref var movableComponent = ref movables.Get(entity);
			ref var dashComponent = ref dashes.Get(entity);
			ref var velocityComponent = ref velocityComponents.Get(entity);
			ref var aimState = ref aimStates.Get(entity);

			var velocity = velocityComponent.velocity;

			movableComponent.relativeVector = Vector3.Normalize(movableComponent.transform.InverseTransformDirection(velocity));

			dashComponent.isStartDash = inputComponent.isDash;

			if (velocity.magnitude == 0)
			{
				continue;
			}

			var speedMove = dashComponent.isActiveDash ? dashComponent.dashSpeed : movableComponent.moveSpeed;
			movableComponent.isActiveDash = dashComponent.isActiveDash;

			if (movableComponent.canMove || movableComponent.isActiveDash)
			{
				characterComponent.characterController.Move(speedMove * Time.deltaTime * velocity);
			}

			if (velocity.magnitude > 0 && aimState.aimState == AimState.NO_AIM)
			{
				velocity.y = 0;
				movableComponent.transform.forward = Vector3.Slerp(characterComponent.characterTransform.forward,
					velocity, movableComponent.coefSmooth);
			}
		}
	}
}
