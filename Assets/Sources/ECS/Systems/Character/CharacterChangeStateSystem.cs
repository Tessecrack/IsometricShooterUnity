using Leopotam.EcsLite;
using UnityEngine;

public class CharacterChangeStateSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<AttackComponent>().Inc<CharacterStateComponent>().End();

		var attacks = world.GetPool<AttackComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();

		foreach(int entity in filter)
		{
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterState = ref characterStates.Get(entity);

			if (attackComponent.isStartAttack) 
			{
				characterState.characterState = CharacterState.ATTACK;
				characterState.passedTime = 0;
			}

			if (characterState.characterState == CharacterState.ATTACK) 
			{
				characterState.passedTime += Time.deltaTime;
			}

			if (characterState.passedTime >= characterState.stateAttackTime)
			{
				characterState.characterState = CharacterState.REST;
				characterState.passedTime = 0;
			}
		}
	}
}
