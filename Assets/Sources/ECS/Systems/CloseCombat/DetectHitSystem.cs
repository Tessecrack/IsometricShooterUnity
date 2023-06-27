using Leopotam.EcsLite;
using UnityEngine;

public class DetectHitSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<EnemyComponent>()
			//.Inc<CloseCombatComponent>()
			.Inc<CharacterComponent>()
			.Inc<HitComponent>()
			.Inc<EnablerComponent>()
			.End();

		var runtimeData = systems.GetShared<SharedData>().RuntimeData;

		var enemies = world.GetPool<EnemyComponent>();
		//var combatComponents = world.GetPool<CloseCombatComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var hitComponents = world.GetPool<HitComponent>();

		foreach(var entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (!enabler.isEnabled)
			{
				continue;
			}

			ref var enemy = ref enemies.Get(entity);
			//ref var combatComponent = ref combatComponents.Get(entity); 
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var hitComponent = ref hitComponents.Get(entity);

			var distanceToPlayer = Vector3.Distance(characterComponent.characterController.transform.position, 
				runtimeData.PlayerPosition);
			
			if (distanceToPlayer <= 2)
			{
				if (runtimeData.PlayerActions.IsPlayerCloseCombatAttack)
				{
					hitComponent.isHitMe = true;
					hitComponent.damageHit = runtimeData.PlayerActions.DamageCloseCombat;
				}
				else
				{
					hitComponent.isHitMe = false;
					hitComponent.wasAppliedDamage = false;
				}
			}
		}
	}
}
