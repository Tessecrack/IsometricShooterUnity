using Leopotam.EcsLite;
using UnityEngine;

public class CharacterChangeStateSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackComponent>()
			.Inc<StateAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var attacks = world.GetPool<AttackComponent>();
		var characterStates = world.GetPool<StateAttackComponent>();
		var enablers = world.GetPool<EnablerComponent>();

		foreach(int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var characterState = ref characterStates.Get(entity);
			if (characterState.isMeleeAttack)
			{
				continue;
			}

			ref var attackComponent = ref attacks.Get(entity);
			if (attackComponent.typeAttack == TypeAttack.Melee)
			{
				characterState.state = CharacterState.Idle;
				characterState.passedTime = 0;
			}

			if (attackComponent.isStartAttack) 
			{
				characterState.state = CharacterState.Aiming;
				characterState.passedTime = 0;
			}

			if (characterState.state == CharacterState.Aiming) 
			{
				characterState.passedTime += Time.deltaTime;
			}

			if (characterState.passedTime >= characterState.stateAttackTime)
			{
				characterState.state = CharacterState.Idle;
				characterState.passedTime = 0;
			}
		}
	}
}
