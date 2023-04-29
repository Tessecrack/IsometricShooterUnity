using Leopotam.Ecs;
using UnityEngine;

public class CharacterChangeStateSystem : IEcsRunSystem
{
	private EcsFilter<AttackComponent, CharacterStateComponent> filter;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var attackComponent = ref filter.Get1(i);
			ref var characterState = ref filter.Get2(i);

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
