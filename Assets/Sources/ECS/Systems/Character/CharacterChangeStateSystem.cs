using Leopotam.EcsLite;
using UnityEngine;

public class CharacterChangeStateSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackComponent>().Inc<StateAttackComponent>().End();

		var attacks = world.GetPool<AttackComponent>();
		var characterStates = world.GetPool<StateAttackComponent>();

		foreach(int entity in filter)
		{
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterState = ref characterStates.Get(entity);

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
				characterState.state = CharacterState.Rest;
				characterState.passedTime = 0;
			}
		}
	}
}
