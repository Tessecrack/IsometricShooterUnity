using Leopotam.EcsLite;
using UnityEngine;

public class CloseCombatMoveSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<CloseCombatComponent>()
			.Inc<CharacterComponent>()
			.Inc<MovableComponent>()
			.Inc<EnablerComponent>()
			.End();

		var combatComponents = world.GetPool<CloseCombatComponent>();
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

			ref var combatComponent = ref combatComponents.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var movable = ref movableComponents.Get(entity);

			if (movable.isActiveDash)
			{
				continue;
			}

			if (combatComponent.closeCombat.NeedForwardMove)
			{
				var speedMove = combatComponent.closeCombat.SpeedMoveForwardInStrike;

				characterComponent.characterController.Move(characterComponent.characterController.transform.forward
					* speedMove * Time.deltaTime);
			}
		}
	}
}
