using Leopotam.EcsLite;
using UnityEngine;

public class MeleeAttackMoveSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<BaseAttackComponent>()
			.Inc<CharacterComponent>()
			.Inc<MovableComponent>()
			.Inc<EnablerComponent>()
			.End();

		var attackEvents = world.GetPool<BaseAttackComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var movableComponents = world.GetPool<MovableComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var attackEvent = ref attackEvents.Get(entity);

			if (attackEvent.baseAttack.NeedForwardMove == false)
			{
				continue;
			}

			ref var movable = ref movableComponents.Get(entity);
			if (movable.isActiveDash)
			{
				continue;
			}

			var speedMove = 40; // TODO: need to improve

			ref var characterComponent = ref characterComponents.Get(entity);
			characterComponent.characterController.Move(characterComponent.characterController.transform.forward
				* speedMove * Time.deltaTime);
		}
	}
}
