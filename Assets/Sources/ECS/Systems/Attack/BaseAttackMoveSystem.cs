using Leopotam.EcsLite;
using UnityEngine;

public class BaseAttackMoveSystem : IEcsRunSystem
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

			ref var movable = ref movableComponents.Get(entity);
			if (movable.isActiveDash)
			{
				continue;
			}

			ref var attackEvent = ref attackEvents.Get(entity);
			if (attackEvent.baseAttack.IsEventAttack)
			{
				if (attackEvent.baseAttack.IsAttackInProcess)
				{
					movable.canMove = false;
				}
				else
				{
					movable.canMove = true;
				}
			}

			var speedMove = 40; // TODO: need to improve
			if (attackEvent.baseAttack.NeedForwardMove == false)
			{
				continue;
			}
			ref var characterComponent = ref characterComponents.Get(entity);
			characterComponent.characterController.Move(characterComponent.characterController.transform.forward
				* speedMove * Time.deltaTime);
		}
	}
}
