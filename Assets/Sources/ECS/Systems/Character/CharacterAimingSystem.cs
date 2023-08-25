using Leopotam.EcsLite;
using UnityEngine;

public class CharacterAimingSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var entities = world.Filter<AimStateComponent>()
			.Inc<AimTimerComponent>()
			.Inc<BaseAttackComponent>()
			.Inc<EnablerComponent>()
			.End();

		var aimStates = world.GetPool<AimStateComponent>();
		var aimTimers = world.GetPool<AimTimerComponent>();
		var baseAttackComponents = world.GetPool<BaseAttackComponent>();
		var enablerComponents = world.GetPool<EnablerComponent>();
	
		foreach (var entity in entities)
		{
			ref var enabler = ref enablerComponents.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var aimState = ref aimStates.Get(entity);
			ref var aimTimer = ref aimTimers.Get(entity);
			ref var baseAttack = ref baseAttackComponents.Get(entity);
			
			if (baseAttack.baseAttack.IsStartAttack)
			{
				aimState.aimState = AimState.AIM;
				aimTimer.passedTime = 0;
			}

			if (aimState.aimState == AimState.AIM)
			{
				aimTimer.passedTime += Time.fixedDeltaTime;
			}

			if (aimTimer.passedTime >= aimTimer.stateAimTime)
			{
				aimState.aimState = AimState.NO_AIM;
				aimTimer.passedTime = 0;
			}
		}
	}
}
